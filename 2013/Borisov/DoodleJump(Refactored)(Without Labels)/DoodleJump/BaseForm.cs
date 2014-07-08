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
    public partial class BaseForm : Form
    {
        private int _hight = 0;
        private DoodleJump _doodlJumper;
        public BaseForm()
        {
            InitializeComponent();
            _doodlJumper = new DoodleJump(ApplicationSettings.StartPositionLeft, ApplicationSettings.StartPositionTop, false, false);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DoodlJumperMove();

            Graphics g = e.Graphics;
            g.DrawImage(new Bitmap("Resources/Jumper.bmp"), _doodlJumper.Left, _doodlJumper.Top + this.AutoScrollPosition.Y);
            g.FillRectangle(Brushes.LimeGreen, ApplicationSettings.BaseLabelPositionX, ApplicationSettings.BaseLabelPositionY, ApplicationSettings.RungsLength, ApplicationSettings.RungsHight);
            Invalidate();

        }

        public void DoodlJumperMove()
        {
            if (_doodlJumper.FlyPhase == false)
            {
                _doodlJumper.Top += ApplicationSettings.TopIncrease;
                System.Threading.Thread.Sleep(ApplicationSettings.Sleep);
                if (_doodlJumper.Top + ApplicationSettings.DoodleHight > ApplicationSettings.FormHight)
                
                    _doodlJumper.Top = 0;
                
            }
            if (((_doodlJumper.Top + ApplicationSettings.DoodleHight == ApplicationSettings.BaseLabelPositionY) ||
                (_doodlJumper.Top + ApplicationSettings.DoodleHight == ApplicationSettings.BaseLabelPositionY + 1)) &&
                (_doodlJumper.FlyPhase == false) && (_doodlJumper.Left < ApplicationSettings.MinimumLabelRightPosition) &&
                (_doodlJumper.Left + ApplicationSettings.DoodleHight > ApplicationSettings.BaseLabelPositionX))
            
                _doodlJumper.FlyPhase = true;
           
            if (_doodlJumper.FlyPhase == true)
            {
                _doodlJumper.Top -= ApplicationSettings.TopIncrease;
                System.Threading.Thread.Sleep(ApplicationSettings.Sleep);
                _hight++;
            }
            if (_hight == ApplicationSettings.MaximumHight)
            {
                _doodlJumper.FlyPhase = false;
                _hight = 0;
            }
        }

        private void BaseForm_Paint(object sender, PaintEventArgs e)
        {
            this.Refresh();

        }

        private void BaseForm_Activated(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void BaseForm_Paint_1(object sender, PaintEventArgs e)
        {
            this.Refresh();
        }

    }
}
