using System;

namespace Championship
{
    [Serializable]
    public class Team
    {
        public string Name { get; set; }

        public Team(string name)
        {
            Name = name;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            return Name.Equals(((Team)obj).Name);
        }
    }
}
