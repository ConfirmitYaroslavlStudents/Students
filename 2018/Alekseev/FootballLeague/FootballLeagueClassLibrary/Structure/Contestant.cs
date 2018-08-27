using System;

namespace FootballLeagueClassLibrary.Structure
{
    [Serializable]
    public class Contestant
    {
        public Contestant()
        {
            
        }

        public string Name;
        public Contestant(string name)
        {
            Name = name;
        }
    }
}
