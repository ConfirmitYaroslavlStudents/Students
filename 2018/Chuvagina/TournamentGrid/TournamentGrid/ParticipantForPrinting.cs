using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentGrid
{
    internal class ParticipantForPrinting
    {
        public string Name;
        public string KindOfBracket;
        public ConsoleColor Color;



        public ParticipantForPrinting(ConsoleColor color)
        {
            Color = color;
        }
    }
}
