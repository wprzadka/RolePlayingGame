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
        private IGameState currentState;
        private IMainMenu mainMenu;

        public Game()
        {
            InitializeComponent();
            mainMenu = new MainMenu(Controls, Width, Height, ChoseClass);
        }

        private void loadEventsList(IList<IAction> actionsList)
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

                IList<IAction> newActionsList;
                act.Click += new EventHandler((sender, e) =>
                    {
                        try
                        {
                            (currentState.Message, newActionsList) = action.Execute(currentState);
                            loadEventsList(newActionsList);
                        }
                        catch (RolePlayingGame.Engine.Exceptions.EndGameException)
                        {
                            //EndGameAction end = new EndGameAction();
                            //(currentState.Message, newActions) = end.Execute(currentState);
                            MessageBox.Show("Game Over");
                        }
                    }
                );
                Controls.Add(act);
            }
        }

        private void GameWindow_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
            mainMenu.loadScreen();
        }

        private void ChoseClass()
        {
            var classNames = new List<string>
            {
                "Warrior", "Archer", "Mage"
            };
            var charactersTiles = new List<Button>();
            var shiftPos = 1;
            foreach (var name in classNames)
            {
                var newTile = new Button
                {
                    Width = Width / 3 - Width / 8,
                    Height = Height - Height / 4,
                    Location = new Point(Width / 4 * shiftPos - Width / 8, Height / 20),
                    Text = name,
                    Name = name,
                    BackColor = Color.AliceBlue,
                    ForeColor = Color.DarkSlateGray
                };
                ++shiftPos;
                charactersTiles.Add(newTile);
            }
            var characters = new List<PlayerCharacter>
            {
                new Warrior("player", new Equipment()),
                new Archer("player", new Equipment()),
                new Mage("player", new Equipment())
            };

            foreach (var (character ,button) in characters.Zip(charactersTiles))
            {
                button.Click += new EventHandler((e, sender) => startGame(character));
            }
            Controls.Clear();
            foreach (var button in charactersTiles)
            {
                Controls.Add(button);
            }
        }

        private void startGame(IPlayerCharacter player)
        {
            // TODO implement auto generation of Zones
            var conversation = new List<IAction> { new ConversationAction("talk with master", "Hello!") };
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
            foreach (IZone location in startLoction)
            {
                root.Neighbours.Add(location);
                root.Actions.Add(new TravelAction(location));
                location.Neighbours.Add(root);
                location.Actions.Add(new TravelAction(root));
            }

            currentState = new GameState(player, root, new Dice(DateTime.Now.Second), new FightLogic());

            this.Paint += new PaintEventHandler(DrawGraph);
            this.Paint += new PaintEventHandler((sender, e) => TextRenderer.DrawText(e.Graphics, currentState.Message, DefaultFont,
                new Rectangle(60, Height / 2, 400, 400), Color.White, TextFormatFlags.Top | TextFormatFlags.EndEllipsis));
            loadEventsList(currentState.Zone.Actions);
        }

        private void DrawGraph(object sender, PaintEventArgs e)
        {
            List<IZone> paths = new List<IZone> { currentState.Zone };
            foreach(IZone v in currentState.Zone.Neighbours)
            {
                paths.Add(v);
            }

            int[] currPos = {currentState.Zone.Position.Item1, currentState.Zone.Position.Item2};
            int diameter = 40;
            Pen liner = new Pen(Color.Black, 6);

            foreach(IZone iter in paths)
            {
                e.Graphics.DrawLine(liner, 
                    new PointF((Width + diameter) / 2, (Height + diameter) / 2),
                    new PointF(iter.Position.Item1 - currPos[0] + (Width + diameter) / 2, iter.Position.Item2 - currPos[1] + (Height + diameter) / 2));

            }
            foreach (IZone iter in paths)
            {
                Rectangle box = new Rectangle(iter.Position.Item1 - currPos[0] + Width / 2, iter.Position.Item2 - currPos[1] + Height / 2, diameter, diameter);

                e.Graphics.FillEllipse(Brushes.AliceBlue, box);
                e.Graphics.DrawEllipse(liner, box);
            }

            TextFormatFlags textFlags = TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis;

            foreach (IZone iter in paths)
            {
                TextRenderer.DrawText(e.Graphics, iter.Name, DefaultFont, 
                    new Rectangle(iter.Position.Item1 - currPos[0] + Width / 2 + diameter, iter.Position.Item2 - currPos[1] + Height / 2 - diameter, 200, 40), Color.White, textFlags);
            }
        }

        private void clock_Tick(object sender, EventArgs e)
        {
            this.Refresh();
        }
    }
}
