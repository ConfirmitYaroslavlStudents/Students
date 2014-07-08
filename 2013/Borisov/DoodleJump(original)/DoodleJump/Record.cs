using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoodleJump
{
    public class Record
    {
        public string name;
        public int score;
        public DateTime dt;

        public Record() { }
        public Record(string n, int s, DateTime d)
        {
            this.name = n;
            this.score = s;
            this.dt = d;
        }
        static public int RecordCompare(Record r1, Record r2)
        {
            return r1.score.CompareTo(r2.score);
        }
    }
}
