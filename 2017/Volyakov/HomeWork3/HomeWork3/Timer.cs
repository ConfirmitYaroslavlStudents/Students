using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace HomeWork3
{
    public abstract class Timer
    {
        protected long time;
        protected Stopwatch timer;

        public Timer ()
        {
            time = 0;
            timer = new Stopwatch();
        }

        public virtual long GetTime()
        {
            return time;
        }
    }
}
