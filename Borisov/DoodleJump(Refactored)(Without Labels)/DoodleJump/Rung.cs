using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoodleJump
{
    public class Rung
    {
        public int Left;
        public int Top;
        public bool Visible;
        public bool MoveToLeft;
        public string Color;

        public Rung(int left, int top, bool visible, string color)
        {
            this.Left = left;
            this.Top = top;
            this.Visible = visible;
            this.Color = color;

        }
       
    }
}
