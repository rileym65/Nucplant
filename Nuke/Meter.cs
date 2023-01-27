using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Nuke
{
    public class Meter : System.Windows.Forms.Panel
    {
        public int min { get; set; }
        public int max { get; set; }
        public int value { get; set; }
        public int lowRed { get; set; }
        public int highRed { get; set; }

        public Meter()
        {
            lowRed = 0;
            highRed = 100;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics gc = e.Graphics;
            Pen pen = new Pen(Color.Black);
            Brush brush = new SolidBrush(Color.Red);
            for (var i = 1; i < 10; i++)
                gc.DrawLine(pen, i * (this.Width / 10), 0, i * (this.Width / 10), this.Height / 10);
            if (lowRed > 0)
            {
                gc.FillRectangle(brush, 0, 20, ((float)lowRed/(float)max) * Width, 10);
            }
            if (highRed > 0 && highRed < 100)
            {
                gc.FillRectangle(brush, ((float)highRed / (float)max) * Width, 20, Width - (((float)highRed / (float)max) * Width), 10);
            }
            gc.DrawLine(pen, ((float)value / (float)max * (float)Width), 15, ((float)value / (float)max * (float)Width), Height);
        }

        public void Value(int i)
        {
            if (i < min) i = min;
            if (i > max) i = max;
            if (i >= min && i<= max && i != value)
            {
                value = i;
                this.Invalidate();
            }
        }
    }
}
