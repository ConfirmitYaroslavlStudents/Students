using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Championship
{
    public class Meeting
    {
        public string Stage;
        public string PlayerFirst;
        public string PlayerSecond;
        public string Score;
        public Meeting NextStage;

        public Meeting()
        {
            PlayerFirst = null;
            PlayerSecond = null;
            Score = "--:--";
        }
    }
}
