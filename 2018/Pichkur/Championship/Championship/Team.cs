using System;

namespace Championship
{
    public class Team
    {
        public string Name { get; set; }
        public int Score { get; private set; }

        public Team()
        {
            Name = null;
            Score = -1;
        }

        public Team(string name)
        {
            Name = name;
            Score = -1;
        }

        public void SetScore()
        {
            Console.Write("Enter {0} team score: ", Name);
            Score = DataValidation.CheckInteger();
        }
    }
}
