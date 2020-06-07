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
        private MapGraph locationsGraph = new MapGraph();

        public Game()
        {
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
            int diameter = 100;
            do{
                Rectangle box = new Rectangle(iter.x, iter.y, iter.x + diameter, iter.y + diameter);

                e.Graphics.FillEllipse(Brushes.Blue, box);
                e.Graphics.DrawEllipse(Pens.Black, box);

            }while(iter.neighbour.Count > 0);
            
        }

        private void clock_Tick(object sender, EventArgs e)
        {
            this.Refresh();
        }
    }
}
