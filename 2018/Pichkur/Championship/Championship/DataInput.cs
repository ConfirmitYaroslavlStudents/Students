using System;
using System.Collections.Generic;

namespace Championship
{
    static class DataInput
    {
        public static int InputIntegerNumber(int minValue)
        {
            var number = -1;

            while (!int.TryParse(Console.ReadLine(), out number) || number < minValue)
            {
                Console.WriteLine(Messages.NotAPositiveIntegerNumber);
                Console.Write(Messages.TryAgain);
            }

            return number;
        }

        public static Team InputCorrectTeam(List<Team> teams)
        {
            var team = new Team(Console.ReadLine());

            while (teams.Contains(team))
            {
                Console.WriteLine(Messages.ImpossibleTeamName);
                Console.Write(Messages.TryAgain);
                team = new Team(Console.ReadLine());
            }

            return team;
        }

        public static void InputGameScore(Game game)
        {
            if (game== null||game.IsPlayed||game.FirstTeam==null||game.SecondTeam==null)
                return;

            int minScoreValue = 0;
            game.IsPlayed = true;
            Console.WriteLine(Messages.ShowOpponents(game.FirstTeam.Name, game.SecondTeam.Name));
            Console.Write(Messages.SetTeamScore(game.FirstTeam.Name));
            game.FirstTeamScore = InputIntegerNumber(minScoreValue);
            Console.Write(Messages.SetTeamScore(game.SecondTeam.Name));
            game.SecondTeamScore = InputIntegerNumber(minScoreValue);
            Console.WriteLine();
        }

        public static List<Team> InputTeams()
        {
            int minCountTeams = 2;
            var teams = new List<Team>();
            Console.Write(Messages.InputCountTeams);

            var countTeams = InputIntegerNumber(minCountTeams);
            Console.WriteLine(Messages.InputNamesOfTeams);

            for (int i = 0; i < countTeams; i++)
            {
                Console.Write(Messages.SomeIndex(i + 1));
                teams.Add(InputCorrectTeam(teams));
            }

            return teams;
        }
    }
}
