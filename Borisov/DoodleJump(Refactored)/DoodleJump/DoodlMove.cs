using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoodleJump
{
    internal class DoodlMove
    {
        public  DoodlMove(){}

        public void DoodlJumperMove(DoodleJump DoodlJumper, System.Windows.Forms.Label DoodleLabel, ref int hight)
        {
            if (DoodlJumper.FlyFaza == false)
            {
                DoodleLabel.Top += 2;
                System.Threading.Thread.Sleep(4);
                if (DoodleLabel.Top + 36 > 350)
                {
                    DoodleLabel.Top = 0;
                }
            }
            if ((DoodleLabel.Top + 36 == 294) && (DoodlJumper.FlyFaza == false) && (DoodleLabel.Location.X < 73) && (DoodleLabel.Location.X + 35 > 28))
            {
                DoodlJumper.FlyFaza = true;
            }
            if ((DoodleLabel.Top + 36 == 295) && (DoodlJumper.FlyFaza == false) && (DoodleLabel.Location.X < 73) && (DoodleLabel.Location.X + 35 > 28))
            {
                DoodlJumper.FlyFaza = true;
            }
            if (DoodlJumper.FlyFaza == true)
            {
                DoodleLabel.Top -= 2;
                System.Threading.Thread.Sleep(4);
                hight++;
            }
            if (hight == 45)
            {
                DoodlJumper.FlyFaza = false;
                hight = 0;
            }
        }
    }
}
