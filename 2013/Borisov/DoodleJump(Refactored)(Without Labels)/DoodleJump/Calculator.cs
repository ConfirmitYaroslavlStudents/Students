using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace DoodleJump
{
    class Calculator
    {

        public DoodleJump DoodlJumper;
        public List<Rung> Rungs;
        private int _step;
        private int _lose = 0;
        private int _hightjump = 0;
        private int _score = 0;
        private Form _menu;
        private Form _game;

        public Calculator( Form menu,Form game)
        {
            this._menu=menu;
            this._game=game;
            SetRungs();
            DoodlJumper = new DoodleJump(ApplicationSettings.StartPositionLeft, ApplicationSettings.Start, false, false);
        }

        private void SetRungs()
        {

            Rungs = new List<Rung>();
            var myFile = new StreamReader("Resources/Map.txt");
            char[] delimited = { ' ', ',', '\t' };
            string temp = myFile.ReadLine();

            while (temp != null)
            {
                string[] Rung = temp.Split(delimited);
                Rungs.Add(new Rung(int.Parse(Rung[0]), int.Parse(Rung[1]), true, Rung[2]));

                temp = myFile.ReadLine();
                temp = myFile.ReadLine();
            }
            myFile.Close();

        }

        public void DoodleJumerGoToRight()
        {

            DoodlJumper.Left += ApplicationSettings.LeftRightIncrease;
            if (DoodlJumper.Left > ApplicationSettings.RightBorder)

                DoodlJumper.Left = 0;


        }

        public void DoodleJumerGoToLeft()
        {
            DoodlJumper.Left -= ApplicationSettings.LeftRightIncrease;
            if (DoodlJumper.Left + ApplicationSettings.DoodleLength <= 0)

                DoodlJumper.Left = ApplicationSettings.RightBorder;

        }

        public void Calculate()
        {
            LoseFormCalling();


            if (DoodlJumper.FlyPhase == false)
            {
                FlyingUp();
            }

            DropOnStep();

            if ((DoodlJumper.FlyPhase == true) && (DoodlJumper.Lose != true))
            {
                FlyingDown();
            }

            BlackStepMove();
        }

        private void LoseFormCalling()
        {
            if ((DoodlJumper.Lose == true) && (_lose < ApplicationSettings.LoseNumber))
            {
                DoodlJumper.Top += 1;
                _lose++;
                _step = _game.AutoScrollPosition.Y;
                _game.AutoScrollPosition = new Point(_game.AutoScrollPosition.X, (-1) * _step + ApplicationSettings.ScrollNumber);
                System.Threading.Thread.Sleep(ApplicationSettings.Sleep);

                if (_lose == ApplicationSettings.LoseNumber)
                {
                    Lose loseform = new Lose(_score, _menu);
                    loseform.Show();
                    _game.Close();
                }
            }
        }

        private void FlyingUp()
        {
            _hightjump = 0;
            DoodlJumper.Top += ApplicationSettings.TopIncrease;

            if ((DoodlJumper.Top - ApplicationSettings.DoodleHight + _game.AutoScrollPosition.Y > ApplicationSettings.BaseLabelPositionY) && (DoodlJumper.Lose != true))
            {
                foreach (Rung p in Rungs)

                    if (p.Top > DoodlJumper.Top - ApplicationSettings.LoseNumber)

                        p.Visible = false;

                DoodlJumper.Lose = true;
            }
            System.Threading.Thread.Sleep(ApplicationSettings.Sleep);

        }

        private void FlyingDown()
        {
            DoodlJumper.Top -= ApplicationSettings.TopIncrease;
            System.Threading.Thread.Sleep(ApplicationSettings.Sleep);
            _hightjump++;
            _step = _game.AutoScrollPosition.Y;

            if (DoodlJumper.Top + _game.AutoScrollPosition.Y < ApplicationSettings.ScrollBorder)

                _game.AutoScrollPosition = new Point(_game.AutoScrollPosition.X, (-1) * _step - ApplicationSettings.LeftRightIncrease);

            if (_hightjump == ApplicationSettings.MaximumHight)

                DoodlJumper.FlyPhase = false;


        }

        private void DropOnStep()
        {
            if (!DoodlJumper.Lose)
            {
                foreach (Rung p in Rungs)
                {

                    if (((DoodlJumper.Top + ApplicationSettings.DoodleHight) == p.Top) && (p.Color != "Red")
                        && (DoodlJumper.Left < p.Left + 45) && (DoodlJumper.Left + ApplicationSettings.DoodleLength > p.Left))

                        DoodlJumper.FlyPhase = true;

                    if (((DoodlJumper.Top + ApplicationSettings.DoodleHight - 1) == p.Top) && (p.Color == "Red")
                        && (DoodlJumper.Left < p.Left + 45) && (DoodlJumper.Left + ApplicationSettings.DoodleLength > p.Left)
                        && (DoodlJumper.FlyPhase == false))

                        p.Visible = false;
                }
                _score = (ApplicationSettings.GameHight + _game.AutoScrollPosition.Y);
            }
        }

        private void BlackStepMove()
        {
            if (!DoodlJumper.Lose)

                foreach (Rung p in Rungs)
                {
                    if (p.Color == "Black")
                    {
                        if (p.MoveToLeft == true)
                            p.Left--;
                        else
                            p.Left++;
                        if (p.Left - ApplicationSettings.RungsHight == 0)
                            p.MoveToLeft = false;
                        if (p.Left + ApplicationSettings.RungsHight + ApplicationSettings.RungsLength == ApplicationSettings.RightBorder)
                            p.MoveToLeft = true;
                    }

                }

        }
    }
}
