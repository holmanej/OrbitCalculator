using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrbitCalculator
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            Click += Form1_Click;
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            ViewPanel viewer = new ViewPanel(Size);
            //viewer.Bodies.Add(new Orbit(2000, 86400 * 30, 2e30)); //ship
            viewer.Bodies.Add(new Orbit(47400, 86400 * 88, 2e30)); //mercury
            viewer.Bodies.Add(new Orbit(35000, 86400 * 224, 2e30)); //venus
            viewer.Bodies.Add(new Orbit(30000, 86400 * 365, 2e30)); //urf
            viewer.Bodies.Add(new Orbit(24000, 86400 * 687, 2e30)); //mars
            viewer.Bodies.Add(new Orbit(18000, 86400 * 467, 2e30)); //ceres
            Controls.Add(viewer);
            viewer.AnimateBodies();
        }
    }
}
