using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DoodleJump
{
    public class Step
        {
            public int x1;
            public int x2;
            public int y;
            public Label label;
            public bool role;
            public bool left;
            
            public Step(int X1, int X2, int Y,Label l,bool r,bool lef)
            {
                this.x1 = X1;
                this.x2 = X2;
                this.y = Y;
                this.label = l;
                this.role = r;
                this.left = lef;
            }
        }
    
}
