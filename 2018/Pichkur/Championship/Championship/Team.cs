namespace Championship
{
    public class Team
    {
        public string Name { get; set; }
        public int Score { get; set; }

        public Team(string name)
        {
            Name = name;
            Score = -1;
        }

        public Team()
        {
            Name = null;
            Score = -1;
        }

        public override bool Equals(object obj)
        {
            return Name.Equals(((Team)obj).Name);
        }
    }
}
