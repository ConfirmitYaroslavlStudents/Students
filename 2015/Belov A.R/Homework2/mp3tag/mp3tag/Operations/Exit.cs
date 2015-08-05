using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mp3tager
{
    class Exit:Operation
    {
        public override void Call()
        {
            Environment.Exit(0);
        }
    }
}
