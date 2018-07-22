using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentGrid
{
    public class ParticipantForPrinting
    {
        public string Name;
        public string Filling;
        public ConsoleColor Color;

        public ParticipantForPrinting(ConsoleColor color)
        {
            Color = color;
        }
    }
}
