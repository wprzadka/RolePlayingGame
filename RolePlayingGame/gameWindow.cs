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
using RolePlayingGame.Engine.Items;
using RolePlayingGame.Engine;
using RolePlayingGame.Engine.Characters.Player;
using RolePlayingGame.Engine.Characters.Player.Classes;
using RolePlayingGame.Engine.Dices;
using RolePlayingGame.Engine.Actions.Fight;

namespace TheRPG
{

    public partial class Game : Form
    {
        private GameState currentState;

        public Game()
        {
            // TODO implement auto generation of Zones
            List<INonPlayerCharacter> npc = new List<INonPlayerCharacter>();
            List<IAction> conversation = new List<IAction>();
            conversation.Add(new ConversationAction("talk with master", "Hello!"));
            npc.Add(new NonPlayerCharacter("Master", 100, 100, 999, 0, 0, new Equipment(), conversation));
            npc.Add(new NonPlayerCharacter("Rabbit", 10, 1, 0, 0, 0, new Equipment(), new List<IAction>()));

            TownZone root = new TownZone("Hideout", "Hideout", new Tuple<int, int>(0, 0), new List<IAction>(), npc);

            root.AddNeighbour(new TownZone("Smith", "Smith", new Tuple<int, int>(100, -50), new List<IAction>(), new List<INonPlayerCharacter>()));
            root.AddNeighbour(new TownZone("Forest", "Forest", new Tuple<int, int>(20, 140), new List<IAction>(), new List<INonPlayerCharacter>()));

            PlayerCharacter player = new Warrior("Player", new Equipment());
            currentState = new GameState(player, root, new Dice(1), new FightLogic());

            InitializeComponent();

            loadEventsList(currentState.Zone.Actions);
        }

        private void loadEventsList(IList<IAction> actionsList)
        {
            Controls.Clear();
            int shiftPos = 1;
            foreach (IAction iter in actionsList)
            {
                Button act = new Button();
                act.Width = 200;
                act.Height = 40;
                act.Location = new Point(80, 40 * shiftPos);
                ++shiftPos;
                act.Text = iter.Name;
                act.Name = iter.Name;

                IList<IAction> newActions;
                act.Click += new EventHandler((object sender, EventArgs e) =>
                    {
                        try
                        {
                            (currentState.Message, newActions) = iter.Execute(currentState);
                            loadEventsList(newActions);
                        }
                        catch (RolePlayingGame.Engine.Exceptions.EndGameException)
                        {
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

            this.Paint += new PaintEventHandler(DrawGraph);
            this.Paint += new PaintEventHandler((object sender, PaintEventArgs e) => TextRenderer.DrawText(e.Graphics, currentState.Message, DefaultFont, 
                new Rectangle(60, Height / 2, 400, 400), Color.White, TextFormatFlags.Top | TextFormatFlags.EndEllipsis));
        }

        private void DrawGraph(object sender, PaintEventArgs e)
        {
            List<IZone> paths = new List<IZone>();
            paths.Add(currentState.Zone);
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
