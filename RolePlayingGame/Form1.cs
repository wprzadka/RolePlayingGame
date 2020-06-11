using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheRPG
{
    public class Location{

        public int x;
        public int y;
        public string name;
        public List<Location> neighbour;

        public Location(int x, int y, string name){
            this.x = x;
            this.y = y;
            this.name = name;
            neighbour = new List<Location>();
        }

        public void addNeighbour(Location location){
            neighbour.Add(location);
        }
    }

    public class MapGraph{

        public Location root = new Location(0, 0, "Hideout");
    }
    public partial class Game : Form
    {
        private MapGraph locationsGraph;

        public Game()
        {
            locationsGraph = new MapGraph();
            locationsGraph.root.addNeighbour(new Location(200, 100, "Forest"));
            locationsGraph.root.addNeighbour(new Location(-200, 200, "Tawern"));
            locationsGraph.root.addNeighbour(new Location(40, -100, "Smith"));

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;

            this.Paint += new PaintEventHandler(DrawGraph);
        }

        private void DrawGraph(object sender, PaintEventArgs e)
        {
            List<Location> paths = new List<Location>();
            paths.Add(locationsGraph.root);
            foreach(Location v in locationsGraph.root.neighbour)
            {
                paths.Add(v);
            }

            int[] currPos = { locationsGraph.root.x, locationsGraph.root.y};
            int diameter = 40;
            Pen liner = new Pen(Color.Black, 6);

            foreach(Location iter in paths)
            {
                e.Graphics.DrawLine(liner, 
                    new PointF(currPos[0] + (Width + diameter) / 2, currPos[1] + (Height + diameter) / 2),
                    new PointF(iter.x + (Width + diameter) / 2, iter.y + (Height + diameter) / 2));

            }
            foreach (Location iter in paths)
            {
                Rectangle box = new Rectangle(iter.x - currPos[0] + Width / 2, iter.y - currPos[1] + Height / 2, diameter, diameter);

                e.Graphics.FillEllipse(Brushes.AliceBlue, box);
                e.Graphics.DrawEllipse(liner, box);
            }

            TextFormatFlags textFlags = TextFormatFlags.Bottom | TextFormatFlags.EndEllipsis;

            foreach (Location iter in paths)
            {
                TextRenderer.DrawText(e.Graphics, iter.name, DefaultFont, 
                    new Rectangle(iter.x - currPos[0] + Width / 2 + diameter, iter.y - currPos[1] + Height / 2 - diameter, 200, 40), Color.White, textFlags);
            }
        }

        private void clock_Tick(object sender, EventArgs e)
        {
            this.Refresh();
        }
    }
}
