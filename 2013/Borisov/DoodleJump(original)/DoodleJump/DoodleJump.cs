using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace DoodleJump
{
    public class DoodleJump
    {
        public int X_left;
        public int X_right;
        public int Y_down;
        public bool FlyFaza;
        public Label DoodleJumper;
        public bool lose;
        public DoodleJump(int x_left, int x_right, int y_down,bool flyfaza,Label doodlejumper,bool l)
        {
            this.X_left = x_left;
            this.X_right = x_right;
            this.Y_down = y_down;
            this.FlyFaza = flyfaza;
            this.DoodleJumper = doodlejumper;
            this.lose = l;

        }
    }
}
