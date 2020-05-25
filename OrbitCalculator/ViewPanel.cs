using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrbitCalculator
{
    class ViewPanel : Panel
    {
        public List<Orbit> Bodies = new List<Orbit>();
        private Timer orbitTimer;
        private double SizeScale = 6e11;
        private double SizeofSun = 1.4e9;

        public ViewPanel(Size formSize)
        {
            Location = new Point(0, 0);
            Size = formSize;
        }

        public void AnimateBodies()
        {
            Graphics gfx = CreateGraphics();
            float sunW = (float)(SizeofSun * Height / SizeScale);
            float sunH = (float)(SizeofSun * Height / SizeScale);
            gfx.FillRectangle(new SolidBrush(Color.White), 0, 0, Width, Height);
            gfx.FillEllipse(new SolidBrush(Color.Orange), Width / 2 - sunW / 2, Height / 2 - sunH / 2, sunW, sunH);

            orbitTimer = new Timer()
            {
                Interval = 16
            };
            orbitTimer.Tick += OrbitTimer_Tick;
            orbitTimer.Start();
        }

        public void FreezeBodies()
        {
            orbitTimer.Stop();
        }

        private void OrbitTimer_Tick(object sender, EventArgs e)
        {
            int speed = 180;
            foreach (Orbit body in Bodies)
            {
                //DrawBody(body, Color.White);
                if (body.tCurrent + speed < body.T / 60)
                {
                    body.tCurrent += speed;
                }
                else
                {
                    body.tCurrent = 0;
                }
                DrawBody(body, Color.Black);
            }
        }

        private void DrawBody(Orbit orbit, Color color)
        {
            double E = orbit.Path[orbit.tCurrent].E;
            double xPos = Height / SizeScale * orbit.a * Math.Cos(E) + Width / 2 - (Height / SizeScale * (orbit.a / 2 - orbit.Rp));
            double yPos = Height / SizeScale * orbit.b * Math.Sin(E) + Height / 2;
            Graphics gfx = CreateGraphics();
            gfx.FillEllipse(new SolidBrush(color), (float)xPos, (float)yPos, 4, 4);
        }
    }
}
