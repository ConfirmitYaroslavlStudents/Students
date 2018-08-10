using System;
using System.Collections.Generic;

namespace Championship
{
    [Serializable]
    public class Round
    {
        public List<Meeting> Meetings = new List<Meeting>();
        public int Stage;
    }
}