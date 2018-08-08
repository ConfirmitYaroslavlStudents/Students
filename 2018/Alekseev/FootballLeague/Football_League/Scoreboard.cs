using System.Collections.Generic;

namespace Football_League
{
    public enum ScoreboardType
    {
        Vertical,
        Horizontal
    }
    class Scoreboard
    {
        private readonly List<string> _matchResultsDisplayGrid1 = new List<string>();
        private readonly List<string> _matchResultsDisplayGrid2 = new List<string>();
        private ScoreboardType _type;

        public int CurrentPlayerPosition = 0;

        public void SetScoreboardType(int num)
        {
            _type = num == 1 ? ScoreboardType.Vertical : ScoreboardType.Horizontal;
        }
        public void AddResults(List<Contestant> winnersGrid1, List<Contestant> winnersGrid2 = null)
        {
            AddResultsGrid(winnersGrid1,_matchResultsDisplayGrid1);
            if(winnersGrid2 != null)
                AddResultsGrid(winnersGrid2,_matchResultsDisplayGrid2);

        }

        private void AddResultsGrid(List<Contestant> winners, List<string> displayGrid)
        {
            string res = "";
            string lowerLine = "";
            int scoreboardIndex = 0;
            int i = 0;

            if (displayGrid.Count != 0)
            {
                while (i < winners.Count)
                {
                    if (scoreboardIndex == winners[i].Position)
                    {
                        lowerLine += "|";
                        i++;
                    }
                    else
                        lowerLine += " ";
                    scoreboardIndex++;
                }
                displayGrid.Add(lowerLine);
            }

            lowerLine = "";
            scoreboardIndex = 0;
            i = 0;

            while (i < winners.Count)
            {
                if (scoreboardIndex == winners[i].Position)
                {
                    res += winners[i].Name;
                    lowerLine += "|";
                    string toAdd = "_";
                    if (i % 2 != 0)
                    {
                        toAdd = " ";
                    }
                    if (i != winners.Count - 1)
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
            displayGrid.Add(res);
            displayGrid.Add(lowerLine);
        }

        public void Print()
        {
            ConsoleWorker.PrintScoreboard(_matchResultsDisplayGrid1,_matchResultsDisplayGrid2, _type);          
        }
    }
}
