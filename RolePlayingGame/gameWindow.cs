using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using RolePlayingGame.Engine.Zones;
using RolePlayingGame.Engine.Actions;
using RolePlayingGame.Engine.Actions.Interactions;
using RolePlayingGame.Engine.Characters.NonPlayer;
using RolePlayingGame.Engine.Items;
using RolePlayingGame.Engine;
using RolePlayingGame.Engine.Characters.Player;
using RolePlayingGame.Engine.Characters.Player.Classes;
using RolePlayingGame.Engine.Dices;

namespace TheRPG
{

    public partial class Game : Form
    {
        private readonly GameState _currentState;

        public Game()
        {
            // TODO implement auto generation of Zones
            var conversation = new List<IAction> { new ConversationAction("talk with master", "Hello!") };
            var npc = new List<INonPlayerCharacter> { 
                new NonPlayerCharacter("Master", 100, 100, 999, 0, 0, new Equipment(), conversation),
                new NonPlayerCharacter("Rabbit", 10, 1, 0, 0, 0, new Equipment(), new List<IAction>())
            };

            var root = new TownZone("Hideout", "Hideout", new Tuple<int, int>(0, 0), new List<IAction>(), npc);

            var startLocation = new List<IZone>{ 
                new TownZone("Smith", "Smith", new Tuple<int, int>(100, -50), new List<IAction>(), new List<INonPlayerCharacter>()),
                new WildZone("Forest", "Forest", new Tuple<int, int>(20, 140), new List<IAction>(), new List<INonPlayerCharacter>())
            };

            foreach (var location in startLocation) {
                root.Neighbours.Add(location);
                location.Neighbours.Add(root);
            }

            PlayerCharacter player = new Warrior("Player", new Equipment());
            _currentState = new GameState(player, root, new Dice(DateTime.Now.Second), new FightLogic());

            InitializeComponent();

            LoadEventsList(_currentState.Zone.Actions);
        }

        private void LoadEventsList(IEnumerable<IAction> actionsList)
        {
            Controls.Clear();
            var shiftPos = 1;
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
                act.Click += (sender, e) =>
                {
                    try
                    {
                        (_currentState.Message, newActionsList) = action.Execute(_currentState);
                        LoadEventsList(newActionsList);
                    }
                    catch (RolePlayingGame.Engine.Exceptions.EndGameException)
                    {
                        //EndGameAction end = new EndGameAction();
                        //(currentState.Message, newActions) = end.Execute(currentState);
                        MessageBox.Show("Game Over");
                    }
                };
                Controls.Add(act);
            }
        }

        private void GameWindow_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;

            Paint += DrawGraph;
            Paint += (sender, e) => TextRenderer.DrawText(e.Graphics, _currentState.Message, DefaultFont, 
                new Rectangle(60, Height / 2, 400, 400), Color.White, TextFormatFlags.Top | TextFormatFlags.EndEllipsis);
        }

        private void DrawGraph(object sender, PaintEventArgs e)
        {
            var paths = new List<IZone> { _currentState.Zone };
            paths.AddRange(_currentState.Zone.Neighbours);

            int[] currentPosition = {_currentState.Zone.Position.Item1, _currentState.Zone.Position.Item2};
            const int diameter = 40;
            var liner = new Pen(Color.Black, 6);

            foreach(var zone in paths)
            {
                e.Graphics.DrawLine(liner, 
                    new PointF((Width + diameter) / 2, (Height + diameter) / 2),
                    new PointF(zone.Position.Item1 - currentPosition[0] + (Width + diameter) / 2, zone.Position.Item2 - currentPosition[1] + (Height + diameter) / 2));

            }

            foreach (var zone in paths)
            {
                var box = new Rectangle(zone.Position.Item1 - currentPosition[0] + Width / 2, zone.Position.Item2 - currentPosition[1] + Height / 2, diameter, diameter);

                e.Graphics.FillEllipse(Brushes.AliceBlue, box);
                e.Graphics.DrawEllipse(liner, box);
            }

            const TextFormatFlags textFlags = TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis;

            foreach (var zone in paths)
            {
                TextRenderer.DrawText(e.Graphics, zone.Name, DefaultFont, 
                    new Rectangle(zone.Position.Item1 - currentPosition[0] + Width / 2 + diameter, zone.Position.Item2 - currentPosition[1] + Height / 2 - diameter, 200, 40), Color.White, textFlags);
            }
        }

        private void clock_Tick(object sender, EventArgs e)
        {
            this.Refresh();
        }
    }
}
