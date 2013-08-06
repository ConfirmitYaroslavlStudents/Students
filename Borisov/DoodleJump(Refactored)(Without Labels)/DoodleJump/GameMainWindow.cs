using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DoodleJump
{
    public partial class GameMainWindow : Form
    {
        private DoodleJump _doodlJumper;
        private List<Rung> _rungs;
        private List<bool> _onBorder;
        private int _i = 0;
        private int _step;
        private int _lose = 0;
        private int _hightjump = 0;
        private int _score = 0;
        private Form _menu;
        public GameMainWindow(Form form)
        {
            this._menu = form;
            InitializeComponent();
            SetRungs();
            _doodlJumper = new DoodleJump(ApplicationSettings.StartPositionLeft, ApplicationSettings.Start, false, false);

        }

        private void SetRungs()
        {
            _onBorder = new List<bool>();
            _rungs = new List<Rung>();
            var myFile = new StreamReader("../../Map.txt");
            char[] delimited = { ' ', ',', '\t' };
            string temp = myFile.ReadLine();

            while (temp != null)
            {
                string[] Rung = temp.Split(delimited);
                _rungs.Add(new Rung(int.Parse(Rung[0]), int.Parse(Rung[1]), true, Rung[2]));
                _onBorder.Add(false);
                temp = myFile.ReadLine();
                temp = myFile.ReadLine();
            }
            myFile.Close();

        }

        private void GameMainWindow_KeyDown(object sender, KeyEventArgs e)
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

            _doodlJumper.Left += ApplicationSettings.LeftRightIncrease;
            if (_doodlJumper.Left > ApplicationSettings.RightBorder)

                _doodlJumper.Left = 0;


        }

        private void DoodleJumerGoToLeft()
        {
            _doodlJumper.Left -= ApplicationSettings.LeftRightIncrease;
            if (_doodlJumper.Left + ApplicationSettings.DoodleLength <= 0)

                _doodlJumper.Left = ApplicationSettings.RightBorder;

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            LoseFormCalling();


            if (_doodlJumper.FlyPhase == false)
            {
                FlyingUp();
            }

            DropOnStep();

            if ((_doodlJumper.FlyPhase == true) && (_doodlJumper.Lose != true))
            {
                FlyingDown();
            }

            BlackStepMove();

            Drawing(e);

            this.Invalidate();
        }

        private void LoseFormCalling()
        {
            if ((_doodlJumper.Lose == true) && (_lose < ApplicationSettings.LoseNumber))
            {
                _doodlJumper.Top += 1;
                _lose++;
                _step = this.AutoScrollPosition.Y;
                this.AutoScrollPosition = new Point(this.AutoScrollPosition.X, (-1) * _step + ApplicationSettings.ScrollNumber);
                System.Threading.Thread.Sleep(ApplicationSettings.Sleep);

                if (_lose == ApplicationSettings.LoseNumber)
                {
                    Lose loseform = new Lose(_score, _menu);
                    loseform.Show();
                    this.Close();
                }
            }
        }

        private void FlyingUp()
        {
            _hightjump = 0;
            _doodlJumper.Top += ApplicationSettings.TopIncrease;

            if ((_doodlJumper.Top - ApplicationSettings.DoodleHight + this.AutoScrollPosition.Y > ApplicationSettings.BaseLabelPositionY) && (_doodlJumper.Lose != true))
            {
                foreach (Rung p in _rungs)

                    if (p.Top > _doodlJumper.Top - ApplicationSettings.LoseNumber)

                        p.Visible = false;

                _doodlJumper.Lose = true;
            }
            System.Threading.Thread.Sleep(ApplicationSettings.Sleep);

        }

        private void FlyingDown()
        {
            _doodlJumper.Top -= ApplicationSettings.TopIncrease;
            System.Threading.Thread.Sleep(ApplicationSettings.Sleep);
            _hightjump++;
            _step = this.AutoScrollPosition.Y;

            if (_doodlJumper.Top + this.AutoScrollPosition.Y < ApplicationSettings.ScrollBorder)

                this.AutoScrollPosition = new Point(this.AutoScrollPosition.X, (-1) * _step - ApplicationSettings.LeftRightIncrease);

            if (_hightjump == ApplicationSettings.MaximumHight)

                _doodlJumper.FlyPhase = false;


        }

        private void DropOnStep()
        {
            if (!_doodlJumper.Lose)
            {
                foreach (Rung p in _rungs)
                {

                    if (((_doodlJumper.Top + ApplicationSettings.DoodleHight) == p.Top) && (p.Color != "Red")
                        && (_doodlJumper.Left < p.Left + 45) && (_doodlJumper.Left + ApplicationSettings.DoodleLength > p.Left))

                        _doodlJumper.FlyPhase = true;

                    if (((_doodlJumper.Top + ApplicationSettings.DoodleHight - 1) == p.Top) && (p.Color == "Red")
                        && (_doodlJumper.Left < p.Left + 45) && (_doodlJumper.Left + ApplicationSettings.DoodleLength > p.Left)
                        && (_doodlJumper.FlyPhase == false))

                        p.Visible = false;
                }
            }
        }

        private void BlackStepMove()
        {
            if (!_doodlJumper.Lose)

                foreach (Rung p in _rungs)
                {
                    if (p.Color == "Black")
                    {
                        if (p.MoveToLeft == true)
                            p.Left--;
                        else
                            p.Left++;
                        if (p.Left - ApplicationSettings.RungsHight == 0)
                            p.MoveToLeft = false;
                        if (p.Left + ApplicationSettings.RungsHight+ApplicationSettings.RungsLength == ApplicationSettings.RightBorder)
                            p.MoveToLeft = true;
                    }

                }

        }

        private void Drawing(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(new Bitmap("../../Джампер1.bmp"), _doodlJumper.Left, _doodlJumper.Top + this.AutoScrollPosition.Y);

            DrawRungs(g);

            if (_doodlJumper.Lose != true)
            {
                g.DrawString("Score:" + (ApplicationSettings.GameHight + this.AutoScrollPosition.Y).ToString(), new Font("Arial", 10), Brushes.Black, new RectangleF(0, 0, 100, 18));
                _score = (ApplicationSettings.GameHight + this.AutoScrollPosition.Y);
            }
        }

        private void DrawRungs(Graphics g)
        {
            foreach (Rung p in _rungs)
            {
                if(p.Visible)
               
                {
                    if (p.Color == "Red")

                        g.FillRectangle(Brushes.Red, p.Left, p.Top + this.AutoScrollPosition.Y, ApplicationSettings.RungsLength, ApplicationSettings.RungsHight);

                    else if (p.Color == "Green")

                        g.FillRectangle(Brushes.LimeGreen, p.Left, p.Top + this.AutoScrollPosition.Y, ApplicationSettings.RungsLength, ApplicationSettings.RungsHight);

                    else

                        g.FillRectangle(Brushes.Black, p.Left, p.Top + this.AutoScrollPosition.Y, ApplicationSettings.RungsLength, ApplicationSettings.RungsHight);
            }
                
            }
        }

        private void GameMainWindow_Activated(object sender, EventArgs e)
        {
            if (_i == 0)

                this.AutoScrollPosition = new Point(0, 4922 - ApplicationSettings.RightBorder);

            _i++;
        }



    }
}
