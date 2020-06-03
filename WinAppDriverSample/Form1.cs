using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinAppDriverSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            panel1.BackColor = Color.Yellow;
        }

        private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            panel1.BackColor = Color.Red;
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.BackColor = Color.Empty;
        }

        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            using (var graphics = panel2.CreateGraphics())
            using (var pen = new Pen(Color.Red))
            {
                graphics.DrawEllipse(pen, e.X, e.Y, 5, 5);
            }
        }
    }
}
