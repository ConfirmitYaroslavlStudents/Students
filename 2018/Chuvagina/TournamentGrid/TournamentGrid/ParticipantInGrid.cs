using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentGrid
{
    public class ParticipantInGrid
    {
        public string Name;
        public ConsoleColor Color;
        public bool IsSeen;

        public ParticipantInGrid(string name, bool isSeen)
        {
            Name = name;
            Color = ConsoleColor.White;
            IsSeen = isSeen;
        }

    }
}
