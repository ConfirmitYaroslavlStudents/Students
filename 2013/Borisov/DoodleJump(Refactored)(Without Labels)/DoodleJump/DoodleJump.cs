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

        public int Left;
        public int Top;
        public bool FlyPhase;
        public bool Lose;
        public DoodleJump(int left, int top, bool flyPhase, bool lose)
        {
            this.Left = left;
            this.Top = top;
            this.FlyPhase = flyPhase;
            this.Lose = lose;
        }
    }
}
