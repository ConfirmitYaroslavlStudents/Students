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
        public DateTime now;

        public Record() { }
        public Record(string name, int score, DateTime now)
        {
            this.name = name;
            this.score = score;
            this.now = now;
        }
        static public int RecordCompare(Record firstRecord, Record secondRecord)
        {
            return firstRecord.score.CompareTo(secondRecord.score);
        }
    }
}
