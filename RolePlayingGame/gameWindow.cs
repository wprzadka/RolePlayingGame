using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RolePlayingGame.Engine.Zones;
using RolePlayingGame.Engine.Actions;
using RolePlayingGame.Engine.Actions.Interactions;
using RolePlayingGame.Engine.Characters.NonPlayer;
using System.Diagnostics;
using System.Reflection.Metadata;
using RolePlayingGame.Engine.Items;
using RolePlayingGame.Engine;
using RolePlayingGame.Engine.Characters.Player;
using RolePlayingGame.Engine.Characters.Player.Classes;
using RolePlayingGame.Engine.Dices;
using RolePlayingGame.Engine.Actions.Fight;
using RolePlayingGame.UserInterface;

namespace TheRPG
{

    public partial class Game : Form
    {
        private IGameState _currentState;
        private readonly IMainMenu _mainMenu;
        private IGameInterface _gameInterface;

        public Game()
        {
            InitializeComponent();
            _mainMenu = new MainMenu(Controls, Width, Height, StartGame, Close);
        }

        private void LoadEventsList(IList<IAction> actionsList)
        {
            Controls.Clear();
            int shiftPos = 1;
            foreach (var action in actionsList)
            {
                var act = new Button {
                    Width = 200, 
                    Height = 40, 
                    Location = new Point(80, 40 * shiftPos), 
                    Text = action.Name, 
                    Name = action.Name, 
                    BackColor = Color.AliceBlue, 
                    ForeColor = Color.DarkSlateGray 
                };
                ++shiftPos;

                string msg;
                IList<IAction> newActionsList;
                act.Click += new EventHandler((sender, e) =>
                    {
                        try
                        {
                            (msg, newActionsList) = action.Execute(_currentState);
                            _currentState.Message = msg + "\n" + _currentState.Message;
                            LoadEventsList(newActionsList);
                        }
                        catch (RolePlayingGame.Engine.Exceptions.EndGameException)
                        {
                            //EndGameAction end = new EndGameAction();
                            //(currentState.Message, newActions) = end.Execute(currentState);
                            MessageBox.Show(@"Game Over");
                            Close();
                        }
                    }
                );
                Controls.Add(act);
            }
        }

        private void GameWindow_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
            _mainMenu.LoadScreen();
        }

        private void StartGame(IPlayerCharacter player)
        {
            // TODO implement auto generation of Zones
            var conversation = new List<IAction> { new ConversationAction("From Narnia", "That's beautiful place") };
            conversation = new List<IAction> { new ConversationAction("Who are you old man?", "Get out fool!"), new ConversationAction("Hello Master", "Where are you coming From?", conversation) };
            conversation = new List<IAction> { new ConversationAction("talk with Master", "Hello!", conversation) };

            var npc = new List<INonPlayerCharacter> {
                new NonPlayerCharacter("Master", 100, 100, 999, 0, 20, new Equipment(), conversation),
                new NonPlayerCharacter("Rabbit", 100, 1, 0, 10, 50, new Equipment(), new List<IAction>())
            };

            var root = new TownZone("Hideout", "Hideout", new Tuple<int, int>(0, 0), new List<IAction>(), npc);

            var monsters = new List<IKillable> {
                new NonPlayerCharacter("Bear", 100, 30, 40, 200, 20, new Equipment(), conversation),
                new NonPlayerCharacter("Wolf", 100, 20, 5, 100, 30, new Equipment(), new List<IAction>())
            };

            var startLoction = new List<IZone>{
                new TownZone("Smith", "Smith", new Tuple<int, int>(100, -50), new List<IAction>(), new List<INonPlayerCharacter>()),
                new WildZone("Forest", "Forest", new Tuple<int, int>(20, 140), new List<IAction>(), monsters)
            };
            foreach (var location in startLoction)
            {
                root.Neighbours.Add(location);
                root.Actions.Add(new TravelAction(location));
                location.Neighbours.Add(root);
                location.Actions.Add(new TravelAction(root));
            }

            _currentState = new GameState(player, root, new Dice(DateTime.Now.Second), new FightLogic());
            _gameInterface = new GameInterface(_currentState, Width, Height, DefaultFont);

            Paint += new PaintEventHandler(DrawGraph);
            Paint += new PaintEventHandler(_gameInterface.DrawUserInterface);

            LoadEventsList(_currentState.Zone.Actions);
        }

        private void DrawGraph(object sender, PaintEventArgs e)
        {
            var paths = new List<IZone> { _currentState.Zone };
            foreach(var v in _currentState.Zone.Neighbours)
            {
                paths.Add(v);
            }

            int[] currPos = {_currentState.Zone.Position.Item1, _currentState.Zone.Position.Item2};
            const int diameter = 40;
            var liner = new Pen(Color.Black, 6);

            foreach(var location in paths)
            {
                e.Graphics.DrawLine(liner, 
                    new PointF((Width + diameter) / 2, (Height + diameter) / 2),
                    new PointF(location.Position.Item1 - currPos[0] + (Width + diameter) / 2, location.Position.Item2 - currPos[1] + (Height + diameter) / 2));

            }
            foreach (var location in paths)
            {
                var box = new Rectangle(location.Position.Item1 - currPos[0] + Width / 2, location.Position.Item2 - currPos[1] + Height / 2, diameter, diameter);

                e.Graphics.FillEllipse(Brushes.AliceBlue, box);
                e.Graphics.DrawEllipse(liner, box);
            }

            const TextFormatFlags textFlags = TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis;

            foreach (var location in paths)
            {
                TextRenderer.DrawText(e.Graphics, location.Name, DefaultFont, 
                    new Rectangle(location.Position.Item1 - currPos[0] + Width / 2 + diameter, location.Position.Item2 - currPos[1] + Height / 2 - diameter, 200, 40), Color.White, textFlags);
            }
        }

        private void clock_Tick(object sender, EventArgs e)
        {
            this.Refresh();
        }
    }
}
