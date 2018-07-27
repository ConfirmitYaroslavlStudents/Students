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

        public static Team CheckTeamName(List<Team> teams)
        {
            var TeamName = new Team(Console.ReadLine());

            while (teams.Contains(TeamName))
            {
                Console.WriteLine(Messages.ImpossibleTeamName);
                Console.Write(Messages.TryAgain);
                TeamName = new Team(Console.ReadLine());
            }

            return TeamName;
        }

        public static void InputGameScore(Game game)
        {
            if (game.FirstTeam.Name == null)
                return;

            Console.WriteLine(Messages.ShowOpponents(game.FirstTeam.Name, game.SecondTeam.Name));

            Console.Write(Messages.SetTeamScore(game.FirstTeam.Name));
            game.FirstTeam.Score = InputIntegerNumber(0);

            Console.Write(Messages.SetTeamScore(game.SecondTeam.Name));
            game.SecondTeam.Score = InputIntegerNumber(0);

            Console.WriteLine();
        }

        public static List<Team> InputTeams()
        {
            var teams = new List<Team>();
            Console.Write(Messages.InputCountTeams);

            var countTeams = InputIntegerNumber(2);
            Console.WriteLine(Messages.InputNamesOfTeams);

            for (int i = 0; i < countTeams; i++)
            {
                Console.Write(Messages.SomeIndex(i + 1));
                teams.Add(CheckTeamName(teams));
            }

            return teams;
        }
    }
}
