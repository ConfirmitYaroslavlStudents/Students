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
    public partial class Menu : Form
    {
        private DoodleJump DoodlJumperMenu;
        private int _hight = 0;
        private bool _isDragging;
        public Menu()
        {
            InitializeComponent();
            DoodlJumperMenu = new DoodleJump(menuDoodle.Location.X, menuDoodle.Location.X + ApplicationSettings.DoodleLength, menuDoodle.Location.Y + ApplicationSettings.DoodleHight, false, menuDoodle, false);

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DoodlMove temp = new DoodlMove();
            temp.DoodlJumperMove(DoodlJumperMenu, menuDoodle, ref _hight);

            Graphics g = e.Graphics;
            g.DrawString("DoodleJump!", new Font("Segoe Script", 20), Brushes.Magenta, new RectangleF(20, 20, 200, 50));

            menuDoodle.Refresh();

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

        private void MenuDoodle_MouseDown(object sender, MouseEventArgs e)
        {
            _isDragging = true;
        }

        private void MenuDoodle_MouseUp(object sender, MouseEventArgs e)
        {
            _isDragging = false;
        }

        private void MenuDoodle_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                menuDoodle.Location = this.PointToClient(Control.MousePosition);
            }
        }

    }
}
