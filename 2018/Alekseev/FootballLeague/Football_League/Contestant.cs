using System;

namespace Football_League
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
