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
        private int _i = 0;
      
        private Form _menu;
        private Calculator _calculator;
        public GameMainWindow(Form form)
        {
            this._menu = form;
            InitializeComponent();
            _calculator = new Calculator(form, this);
        }

    

        private void GameMainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Left")
            {
                _calculator.DoodleJumerGoToLeft();
            }
            if (e.KeyCode.ToString() == "Right")
            {
                _calculator.DoodleJumerGoToRight();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            _calculator.Calculate();

            Drawing(e);

            this.Invalidate();
        }

      

        private void Drawing(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(new Bitmap("Resources/Jumper.bmp"), _calculator.DoodlJumper.Left, _calculator.DoodlJumper.Top + this.AutoScrollPosition.Y);

            DrawRungs(g);

            if (_calculator.DoodlJumper.Lose != true)
            {
                g.DrawString("Score:" + (ApplicationSettings.GameHight + this.AutoScrollPosition.Y).ToString(), new Font("Arial", 10), Brushes.Black, new RectangleF(0, 0, 100, 18));
               
            }
        }

        private void DrawRungs(Graphics g)
        {
            foreach (Rung p in _calculator.Rungs)
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
