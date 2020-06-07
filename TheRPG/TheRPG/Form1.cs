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
        public List<Location> neighbour;

        public Location(int x, int y){
            this.x = x;
            this.y = y;
            neighbour = new List<Location>();
        }

        public void addNeighbour(Location location){
            neighbour.Add(location);
        }
    }

    public class MapGraph{

        public Location root = new Location(0, 0);
    }
    public partial class Game : Form
    {
        private MapGraph locationsGraph;

        public Game()
        {
            locationsGraph = new MapGraph();
            locationsGraph.root.addNeighbour(new Location(200, 100));
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;

            this.Paint += new PaintEventHandler(DrawGraph);
        }

        private void DrawGraph(object sender, PaintEventArgs e)
        {
            Location iter = locationsGraph.root;
            int[] currPos = {iter.x, iter.y};
            int diameter = 40;
            bool runFlag = true;

            while (runFlag)
            {
                Rectangle box = new Rectangle(iter.x - currPos[0] + Width/2, iter.y - currPos[0] + Height/2, diameter, diameter);

                e.Graphics.FillEllipse(Brushes.AliceBlue, box);
                e.Graphics.DrawEllipse(Pens.Black, box);

                // TODO implement move over the visible part of graph and draw locations
                if(iter.neighbour.Count > 0)
                {
                    iter = iter.neighbour[0];
                }
                else
                {
                    runFlag = false;
                }
            };
            
        }

        private void clock_Tick(object sender, EventArgs e)
        {
            this.Refresh();
        }
    }
}
