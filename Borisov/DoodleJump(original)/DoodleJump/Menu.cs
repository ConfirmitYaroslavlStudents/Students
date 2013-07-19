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
        private DoodleJump DoodlJumper2;
        private int hight=0;
        private bool isDragging;
        public Menu()
        {
            InitializeComponent();
            DoodlJumper2 = new DoodleJump(Doodle2.Location.X, Doodle2.Location.X + 26, Doodle2.Location.Y + 36, false, Doodle2, false);
            Step step1 = new Step(label5.Location.X, label5.Location.X + 45, label5.Location.Y, label5, true, false);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (DoodlJumper2.FlyFaza == false)
            {
                Doodle2.Top += 2;
                System.Threading.Thread.Sleep(4);
                if (Doodle2.Top + 36 > 350)
                {
                    Doodle2.Top = 0;
                }
            }
            if ((Doodle2.Top + 36 == 294) && (DoodlJumper2.FlyFaza == false) && (Doodle2.Location.X < 73) && (Doodle2.Location.X+35 >28))
            {
                DoodlJumper2.FlyFaza = true;
            }
            if ((Doodle2.Top + 36 == 295) && (DoodlJumper2.FlyFaza == false) && (Doodle2.Location.X < 73) && (Doodle2.Location.X + 35 > 28))
            {
                DoodlJumper2.FlyFaza = true;
            }
            if (DoodlJumper2.FlyFaza == true)
            {
                Doodle2.Top -= 2;
                System.Threading.Thread.Sleep(4);
                hight++;
            }
            if (hight == 45)
            {
                DoodlJumper2.FlyFaza = false;
                hight = 0;
            }
            Graphics g = e.Graphics;
            g.DrawString("DoodleJump!", new Font("Segoe Script", 20), Brushes.Magenta, new RectangleF(20, 20, 200, 50));
            Invalidate();
            Doodle2.Refresh();
            label5.Refresh();
 
        }
        private void Menu_Paint(object sender, PaintEventArgs e)
        {
            Invalidate();
        }

        private void Menu_Activated(object sender, EventArgs e)
        {
            this.Refresh();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            ListTetradi l = new ListTetradi(this);
            l.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Records r = new Records(this);
            r.Show();
        }

        private void Doodle2_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {

                Doodle2.Location = this.PointToClient(Control.MousePosition);
            }
        }

        private void Doodle2_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void Doodle2_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
        }

       

        
    }
}
