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
        public int XLeft;
        public int XRight;
        public int YDown;
        public bool FlyPhase;
        public Label DoodleJumper;
        public bool Lose;
        public DoodleJump(int xLeft, int xRight, int yDown, bool flyPhase, Label doodlejumper, bool lose)
        {
            this.XLeft = xLeft;
            this.XRight = xRight;
            this.YDown = yDown;
            this.FlyPhase = flyPhase;
            this.DoodleJumper = doodlejumper;
            this.Lose = lose;

        }
    }
}
