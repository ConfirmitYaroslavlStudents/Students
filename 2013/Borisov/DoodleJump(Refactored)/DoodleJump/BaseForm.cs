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
        private bool _isDragging;
        private int _hight = 0;
        private DoodleJump _DoodlJumper;
        public BaseForm()
        {
            InitializeComponent();
            _DoodlJumper = new DoodleJump(DoodleBase.Location.X, DoodleBase.Location.X + ApplicationSettings.DoodleLength, DoodleBase.Location.Y + ApplicationSettings.DoodleHight, false, DoodleBase, false);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DoodlJumperMove();
        }

        public void DoodlJumperMove()
        {
            if (_DoodlJumper.FlyPhase == false)
            {
                DoodleBase.Top += ApplicationSettings.TopIncrease;
                System.Threading.Thread.Sleep(ApplicationSettings.Sleep);
                if (DoodleBase.Top + ApplicationSettings.DoodleHight > ApplicationSettings.TopBorder)
                {
                    DoodleBase.Top = 0;
                }
            }
            if (((DoodleBase.Top + ApplicationSettings.DoodleHight == ApplicationSettings.BaseLabelPositionY) ||
                (DoodleBase.Top + ApplicationSettings.DoodleHight == ApplicationSettings.BaseLabelPositionY + 1)) &&
                (_DoodlJumper.FlyPhase == false) && (DoodleBase.Location.X < ApplicationSettings.MinimumLabelRightPosition) &&
                (DoodleBase.Location.X + ApplicationSettings.DoodleHight > ApplicationSettings.BaseLabelPositionX))
            {
                _DoodlJumper.FlyPhase = true;
            }
            if (_DoodlJumper.FlyPhase == true)
            {
                DoodleBase.Top -= ApplicationSettings.TopIncrease;
                System.Threading.Thread.Sleep(ApplicationSettings.Sleep);
                _hight++;
            }
            if (_hight == ApplicationSettings.MaximumHight)
            {
                _DoodlJumper.FlyPhase = false;
                _hight = 0;
            }
        }

        private void BaseForm_Paint(object sender, PaintEventArgs e)
        {
            Invalidate();

        }

        private void BaseForm_Activated(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void DoodleBase_MouseDown(object sender, MouseEventArgs e)
        {
            _isDragging = true;
        }

        private void DoodleBase_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                DoodleBase.Location = this.PointToClient(Control.MousePosition);
            }

        }

        private void DoodleBase_MouseUp(object sender, MouseEventArgs e)
        {

            _isDragging = false;
        }

    }
}
