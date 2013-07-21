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
    public partial class ListTetradi : Form
    {
        private DoodleJump DoodlJumper;
        private List<Step>[] steps;
        private int maxstep = 0;
        private int i = 0;
        private int step;
        private int lose = 0;
        private int hightjump = 0;
        private int score = 0;
        private Form menu;
        public ListTetradi(Form f)
        {
            this.menu = f;
            InitializeComponent();
            maxStepGet();
            stepsGet();
            DoodlJumper = new DoodleJump(DoodleJumper.Location.X, DoodleJumper.Location.X + 26, DoodleJumper.Location.Y + 36, false, DoodleJumper, false);

        }

        private void maxStepGet()
        {
            foreach (Control p in Controls)
            {
                if ((p.Name != DoodleJumper.Name) && (p.GetType() == typeof(Label)))
                {
                    if (p.Top / 250 > maxstep)
                        maxstep = p.Top / 250;
                }

            }
        }

        private void stepsGet()
        {
            steps = new List<Step>[maxstep + 1];

            for (int j = 0; j <= maxstep; j++)
            {
                steps[j] = new List<Step>();
            }
            foreach (Control p in Controls)
            {
                if ((p.Name != DoodleJumper.Name) && (p.GetType() == typeof(Label)))
                {
                    for (int j = 0; j <= maxstep; j++)
                    {
                        if (p.Top / 250 == j)
                        {
                            steps[j].Add(new Step(p.Location.X, p.Location.X + 45, p.Location.Y, (Label)p, p.BackColor == Color.Black, false));
                        }

                    }
                }

            }
        }

        private void ListTetradi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Left")
            {
                DoodleJumerGoToLeft();
            }
            if (e.KeyCode.ToString() == "Right")
            {
                DoodleJumerGoToRight();
            }
        }

        private void DoodleJumerGoToRight()
        {
            DoodleJumper.Left += 3;
            if (DoodleJumper.Location.X > 250)
            {
                DoodleJumper.Left = 0;
            }
        }

        private void DoodleJumerGoToLeft()
        {
            DoodleJumper.Left -= 3;
            if (DoodleJumper.Location.X + 26 <= 0)
            {
                DoodleJumper.Left = 250;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            LoseFormCalling();


            if (DoodlJumper.FlyFaza == false)
            {
                FlyingUp();
            }

            DropOnStep();

            if ((DoodlJumper.FlyFaza == true) && (DoodlJumper.lose != true))
            {
                FlyingDown();
            }

            BlackStepMove();

            DoodlAndStepsRefreshing();

            DrawScore(e);

            this.Invalidate();

            base.OnPaint(e);


        }

        private void LoseFormCalling()
        {

            if ((DoodlJumper.lose == true) && (lose < 100))
            {
                DoodleJumper.Top += 1;
                if (DoodleJumper.Top < 0)
                    DoodleJumper.Visible = false;
                System.Threading.Thread.Sleep(4);
                step = this.AutoScrollPosition.Y;
                this.AutoScrollPosition = new Point(this.AutoScrollPosition.X, (-1) * step + 7);
                DoodleJumper.Refresh();
                lose++;
                if (lose == 100)
                {

                    Lose loseform = new Lose(score, menu);
                    loseform.Show();
                    this.Close();
                }
            }
        }

        private void FlyingUp()
        {
            hightjump = 0;
            DoodleJumper.Top += 2;
            if ((DoodleJumper.Location.Y - 36 > 290) && (DoodlJumper.lose != true))
            {
                foreach (Control p in Controls)
                {
                    if ((p.Name != DoodleJumper.Name) && (p.GetType() == typeof(Label)) && (p.Location.Y > DoodleJumper.Location.Y - 100))
                    {
                        p.Visible = false;
                    }
                }
                DoodlJumper.lose = true;
            }
            System.Threading.Thread.Sleep(4);
        }

        private void FlyingDown()
        {

            DoodleJumper.Top -= 2;
            System.Threading.Thread.Sleep(4);
            hightjump++;
            step = this.AutoScrollPosition.Y;
            if (DoodleJumper.Top < 200)
            {
                this.AutoScrollPosition = new Point(this.AutoScrollPosition.X, (-1) * step - 3);
            }
            if (hightjump == 45)
            {
                DoodlJumper.FlyFaza = false;
            }
        }

        private void DropOnStep()
        {
            if (!DoodlJumper.lose)
            {
                foreach (Step s in steps[(DoodleJumper.Top + (-1) * this.AutoScrollPosition.Y + 36) / 250])
                {

                    if (((DoodleJumper.Top + (-1) * this.AutoScrollPosition.Y + 36) == s.y) && (DoodleJumper.Location.X < s.x2) && (DoodleJumper.Location.X + 26 > s.x1))
                    {
                        DoodlJumper.FlyFaza = true;
                    }
                    if (((DoodleJumper.Top + (-1) * this.AutoScrollPosition.Y + 37) == s.y) && (DoodleJumper.Location.X < s.x2) && (DoodleJumper.Location.X + 26 > s.x1) && (DoodlJumper.FlyFaza == false))
                    {
                        s.label.Visible = false;
                    }
                }
            }
        }

        private void BlackStepMove()
        {
            if (DoodlJumper.lose != true)

                foreach (Step p in steps[(DoodleJumper.Top + (-1) * this.AutoScrollPosition.Y + 51) / 250])
                {
                    if ((p.role == true) && (p.left == true))
                    {
                        p.label.Left++;
                        p.x1++;
                        p.x2++;
                        if (p.label.Left > 200)
                        {
                            p.left = false;
                        }
                    }
                    if ((p.role == true) && (p.left == false))
                    {
                        p.label.Left--;
                        p.x1--;
                        p.x2--;
                        if (p.label.Left < 30)
                        {
                            p.left = true;
                        }
                    }
                }
            foreach (Step p in steps[(DoodleJumper.Top + (-1) * this.AutoScrollPosition.Y - 200) / 250])
            {
                if ((p.role == true) && (p.left == true))
                {
                    p.label.Left++;
                    p.x1++;
                    p.x2++;
                    if (p.label.Left > 200)
                    {
                        p.left = false;
                    }
                }
                if ((p.role == true) && (p.left == false))
                {
                    p.label.Left--;
                    p.x1--;
                    p.x2--;
                    if (p.label.Left < 30)
                    {
                        p.left = true;
                    }
                }
            }
        }

        private void DoodlAndStepsRefreshing()
        {
            DoodleJumper.Refresh();
            if (DoodlJumper.lose != true)
            {
                foreach (Step p in steps[(DoodleJumper.Top + (-1) * this.AutoScrollPosition.Y + 36) / 250])
                {
                    p.label.Refresh();

                }
                foreach (Step p in steps[(DoodleJumper.Top + (-1) * this.AutoScrollPosition.Y - 214) / 250])
                {
                    p.label.Refresh();

                }
            }
        }

        private void DrawScore(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (DoodlJumper.lose != true)
            {
                g.DrawString("Score:" + (4672 + this.AutoScrollPosition.Y).ToString(), new Font("Arial", 10), Brushes.Black, new RectangleF(0, 0, 100, 18));
                score = (4672 + this.AutoScrollPosition.Y);
            }
        }

        private void ListTetradi_Activated(object sender, EventArgs e)
        {
            if (i == 0)
            {
                this.AutoScrollPosition = new Point(0, DoodleJumper.Location.Y - 250);
                this.Refresh();
            }
            i++;
        }

    }
}
