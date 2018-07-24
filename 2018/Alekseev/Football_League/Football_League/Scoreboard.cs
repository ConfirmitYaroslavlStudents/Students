using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football_League
{
    class Scoreboard
    {
        readonly List<string> _matchResultsDisplay = new List<string>();
        public int currentPlayerPosition = 0;
        public void AddResults(List<Contestant> contestants)
        {
            string res = "";
            string lowerLine = "";
            int scoreboardIndex = 0;
            int i = 0;

            if (_matchResultsDisplay.Count != 0)
            {
                while (i < contestants.Count)
                {
                    if (scoreboardIndex == contestants[i].position)
                    {
                        lowerLine += "|";
                        i++;
                    }
                    else
                        lowerLine += " ";
                    scoreboardIndex++;
                }             
                _matchResultsDisplay.Add(lowerLine);
            }

            lowerLine = "";
            scoreboardIndex = 0;
            i = 0;

            while (i < contestants.Count)
            {
                if (scoreboardIndex == contestants[i].position)
                {
                    res += contestants[i].Name;
                    lowerLine += "|";
                    string toAdd = "_";
                    if (i % 2 != 0)
                    {
                        toAdd = " ";
                    }
                    if (i != contestants.Count - 1)
                    {
                        for (int j = scoreboardIndex + 1; j < res.Length; j++)
                            lowerLine += toAdd;
                    }
                    scoreboardIndex = res.Length;
                    i++;
                }
                else
                {
                    res += " ";
                    if (i % 2 != 0)
                        lowerLine += "_";
                    else
                        lowerLine += " ";
                    scoreboardIndex++;
                }
            }
            _matchResultsDisplay.Add(res);
            if(contestants.Count != 1)
                _matchResultsDisplay.Add(lowerLine);           
        }

        public void Print()
        {
            foreach (var round in _matchResultsDisplay)
                Console.WriteLine(round);
            Console.WriteLine("To continue press Enter...");
            while (Console.ReadKey().Key != ConsoleKey.Enter)
                continue;
            Console.Clear();
        }
    }
}
