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
using RolePlayingGame.Engine.Characters.NonPlayer;

namespace TheRPG
{

    public partial class Game : Form
    {
        private ZoneBase root;

        public Game()
        {
            root = new TownZone("Hideout", "Hideout", new Tuple<int, int>(0, 0), new List<IAction>(), new List<INonPlayerCharacter>());

            root.Neighbours.Add(new TownZone("Smith", "Smith", new Tuple<int, int>(100, -50), new List<IAction>(), new List<INonPlayerCharacter>()));
            root.Neighbours.Add(new TownZone("Forest", "Forest", new Tuple<int, int>(20, 140), new List<IAction>(), new List<INonPlayerCharacter>()));

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;

            this.Paint += new PaintEventHandler(DrawGraph);
        }

        private void DrawGraph(object sender, PaintEventArgs e)
        {
            List<IZone> paths = new List<IZone>();
            paths.Add(root);
            foreach(IZone v in root.Neighbours)
            {
                paths.Add(v);
            }

            int[] currPos = {root.Position.Item1, root.Position.Item2};
            int diameter = 40;
            Pen liner = new Pen(Color.Black, 6);

            foreach(IZone iter in paths)
            {
                e.Graphics.DrawLine(liner, 
                    new PointF(currPos[0] + (Width + diameter) / 2, currPos[1] + (Height + diameter) / 2),
                    new PointF(iter.Position.Item1 + (Width + diameter) / 2, iter.Position.Item2 + (Height + diameter) / 2));

            }
            foreach (IZone iter in paths)
            {
                Rectangle box = new Rectangle(iter.Position.Item1 - currPos[0] + Width / 2, iter.Position.Item2 - currPos[1] + Height / 2, diameter, diameter);

                e.Graphics.FillEllipse(Brushes.AliceBlue, box);
                e.Graphics.DrawEllipse(liner, box);
            }

            TextFormatFlags textFlags = TextFormatFlags.Bottom | TextFormatFlags.EndEllipsis;

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
