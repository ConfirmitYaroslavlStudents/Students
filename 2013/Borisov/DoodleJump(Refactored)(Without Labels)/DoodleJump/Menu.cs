using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DoodleJump
{
    public partial class Menu : BaseForm
    {

        public Menu()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.DrawString("DoodleJump!", new Font("Segoe Script", 20), Brushes.Magenta, new RectangleF(20, 20, 200, 50));
        
        }

        private void Menu_Paint(object sender, PaintEventArgs e)
        {
            Invalidate();

        }

        private void Menu_Activated(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            GameMainWindow l = new GameMainWindow(this);
            l.Show();
        }

        private void RecordsButton_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Records r = new Records(this);
            r.Show();
        }

    }
}
