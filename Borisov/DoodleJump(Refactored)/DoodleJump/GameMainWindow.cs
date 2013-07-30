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
    public partial class GameMainWindow : Form
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
        public GameMainWindow(Form form)
        {
            this.menu = form;
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
                    if (p.Top / ApplicationSettings.StepsCount > maxstep)
                        maxstep = p.Top / ApplicationSettings.StepsCount;
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
                        if (p.Top / ApplicationSettings.StepsCount == j)
                        {
                            steps[j].Add(new Step(p.Location.X, p.Location.X + ApplicationSettings.StepsLength, p.Location.Y, (Label)p, p.BackColor == Color.Black, false));
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
            DoodleJumper.Left += ApplicationSettings.LeftRightIncrease;
            if (DoodleJumper.Location.X > ApplicationSettings.RightBorder)
            {
                DoodleJumper.Left = 0;
            }
        }

        private void DoodleJumerGoToLeft()
        {
            DoodleJumper.Left -= ApplicationSettings.LeftRightIncrease;
            if (DoodleJumper.Location.X + ApplicationSettings.DoodleLength <= 0)
            {
                DoodleJumper.Left = ApplicationSettings.RightBorder;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            LoseFormCalling();


            if (DoodlJumper.FlyPhase == false)
            {
                FlyingUp();
            }

            DropOnStep();

            if ((DoodlJumper.FlyPhase == true) && (DoodlJumper.lose != true))
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

            if ((DoodlJumper.lose == true) && (lose < ApplicationSettings.LoseNumber))
            {
                DoodleJumper.Top += 1;
                if (DoodleJumper.Top < 0)
                    DoodleJumper.Visible = false;
                System.Threading.Thread.Sleep(ApplicationSettings.Sleep);
                step = this.AutoScrollPosition.Y;
                this.AutoScrollPosition = new Point(this.AutoScrollPosition.X, (-1) * step + ApplicationSettings.ScrollNumber);
                DoodleJumper.Refresh();
                lose++;
                if (lose == ApplicationSettings.LoseNumber)
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
            DoodleJumper.Top += ApplicationSettings.TopIncrease;
            if ((DoodleJumper.Location.Y - ApplicationSettings.DoodleHight > ApplicationSettings.LabelTopBorder) && (DoodlJumper.lose != true))
            {
                foreach (Control p in Controls)
                {
                    if ((p.Name != DoodleJumper.Name) && (p.GetType() == typeof(Label))
                        && (p.Location.Y > DoodleJumper.Location.Y - ApplicationSettings.LoseNumber))
                    {
                        p.Visible = false;
                    }
                }
                DoodlJumper.lose = true;
            }
            System.Threading.Thread.Sleep(ApplicationSettings.Sleep);
        }

        private void FlyingDown()
        {

            DoodleJumper.Top -= ApplicationSettings.TopIncrease;
            System.Threading.Thread.Sleep(ApplicationSettings.Sleep);
            hightjump++;
            step = this.AutoScrollPosition.Y;
            if (DoodleJumper.Top < ApplicationSettings.StepsRightBorder)
            {
                this.AutoScrollPosition = new Point(this.AutoScrollPosition.X, (-1) * step - ApplicationSettings.LeftRightIncrease);
            }
            if (hightjump == ApplicationSettings.MaximumHight)
            {
                DoodlJumper.FlyPhase = false;
            }
        }

        private void DropOnStep()
        {
            if (!DoodlJumper.lose)
            {
                foreach (Step step in steps[(DoodleJumper.Top + (-1) * this.AutoScrollPosition.Y + ApplicationSettings.DoodleHight) / ApplicationSettings.StepsCount])
                {

                    if (((DoodleJumper.Top + (-1) * this.AutoScrollPosition.Y + ApplicationSettings.DoodleHight) == step.y)
                        && (DoodleJumper.Location.X < step.x2) && (DoodleJumper.Location.X + ApplicationSettings.DoodleLength > step.x1))
                    {
                        DoodlJumper.FlyPhase = true;
                    }
                    if (((DoodleJumper.Top + (-1) * this.AutoScrollPosition.Y + ApplicationSettings.DoodleHight+1) == step.y)
                        && (DoodleJumper.Location.X < step.x2) && (DoodleJumper.Location.X + ApplicationSettings.DoodleLength > step.x1) 
                        && (DoodlJumper.FlyPhase == false))
                    {
                        step.label.Visible = false;
                    }
                }
            }
        }

        private void BlackStepMove()
        {
            if (DoodlJumper.lose != true)

                foreach (Step p in steps[(DoodleJumper.Top + (-1) * this.AutoScrollPosition.Y + ApplicationSettings.ScrollSecondNumber) / ApplicationSettings.StepsCount])
                {
                    if ((p.role == true) && (p.left == true))
                    {
                        p.label.Left++;
                        p.x1++;
                        p.x2++;
                        if (p.label.Left > ApplicationSettings.StepsRightBorder)
                        {
                            p.left = false;
                        }
                    }
                    if ((p.role == true) && (p.left == false))
                    {
                        p.label.Left--;
                        p.x1--;
                        p.x2--;
                        if (p.label.Left < ApplicationSettings.StepsLeftBorder)
                        {
                            p.left = true;
                        }
                    }
                }
            foreach (Step p in steps[(DoodleJumper.Top + (-1) * this.AutoScrollPosition.Y - ApplicationSettings.StepsRightBorder) / ApplicationSettings.StepsCount])
            {
                if ((p.role == true) && (p.left == true))
                {
                    p.label.Left++;
                    p.x1++;
                    p.x2++;
                    if (p.label.Left > ApplicationSettings.StepsRightBorder)
                    {
                        p.left = false;
                    }
                }
                if ((p.role == true) && (p.left == false))
                {
                    p.label.Left--;
                    p.x1--;
                    p.x2--;
                    if (p.label.Left < ApplicationSettings.StepsLeftBorder)
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
                foreach (Step p in steps[(DoodleJumper.Top + (-1) * this.AutoScrollPosition.Y + ApplicationSettings.DoodleHight) / ApplicationSettings.StepsCount])
                {
                    p.label.Refresh();

                }
                foreach (Step p in steps[(DoodleJumper.Top + (-1) * this.AutoScrollPosition.Y + ApplicationSettings.DoodleHight - ApplicationSettings.RightBorder) / ApplicationSettings.StepsCount])
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
                g.DrawString("Score:" + (ApplicationSettings.GameHight + this.AutoScrollPosition.Y).ToString(), new Font("Arial", 10), Brushes.Black, new RectangleF(0, 0, 100, 18));
                score = (ApplicationSettings.GameHight + this.AutoScrollPosition.Y);
            }
        }

        private void ListTetradi_Activated(object sender, EventArgs e)
        {
            if (i == 0)
            {
                this.AutoScrollPosition = new Point(0, DoodleJumper.Location.Y - ApplicationSettings.RightBorder);
                this.Refresh();
            }
            i++;
        }

    }
}
