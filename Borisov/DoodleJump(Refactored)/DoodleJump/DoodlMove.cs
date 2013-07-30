using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoodleJump
{
    internal class DoodlMove
    {
        public DoodlMove() { }

        public void DoodlJumperMove(DoodleJump DoodlJumper, System.Windows.Forms.Label DoodleLabel, ref int hight)
        {
            if (DoodlJumper.FlyPhase == false)
            {
                DoodleLabel.Top += ApplicationSettings.TopIncrease;
                System.Threading.Thread.Sleep(ApplicationSettings.Sleep);
                if (DoodleLabel.Top + ApplicationSettings.DoodleHight > ApplicationSettings.TopBorder)
                {
                    DoodleLabel.Top = 0;
                }
            }
            if (((DoodleLabel.Top + ApplicationSettings.DoodleHight == ApplicationSettings.LabelTopBorder) ||
                (DoodleLabel.Top + ApplicationSettings.DoodleHight == ApplicationSettings.LabelTopBorder + 1)) &&
                (DoodlJumper.FlyPhase == false) && (DoodleLabel.Location.X < ApplicationSettings.MinimumLabelRightPosition) &&
                (DoodleLabel.Location.X + ApplicationSettings.DoodleHight > ApplicationSettings.MinimumLabelHight))
            {
                DoodlJumper.FlyPhase = true;
            }
            if (DoodlJumper.FlyPhase == true)
            {
                DoodleLabel.Top -= ApplicationSettings.TopIncrease;
                System.Threading.Thread.Sleep(ApplicationSettings.Sleep);
                hight++;
            }
            if (hight == ApplicationSettings.MaximumHight)
            {
                DoodlJumper.FlyPhase = false;
                hight = 0;
            }
        }
    }
}
