using System.Collections.Generic;

namespace Championship
{
    public static class DataInput
    {
        private static IDataInputWorker _consoleWorker=new ConsoleWorker();

        public static int InputIntegerNumber(int minValue)
        {
            var number = -1;

            while (!int.TryParse(_consoleWorker.InputString(), out number) || number < minValue)
            {
                _consoleWorker.WriteLineMessage(Messages.NotAPositiveIntegerNumber);
                _consoleWorker.WriteMessage(Messages.TryAgain);
            }

            return number;
        }

        public static Team InputCorrectTeam(List<Team> teams)
        {
            var team = new Team(_consoleWorker.InputString());

            while (teams.Contains(team))
            {
                _consoleWorker.WriteLineMessage(Messages.ImpossibleTeamName);
                _consoleWorker.WriteMessage(Messages.TryAgain);
                team = new Team(_consoleWorker.InputString());
            }

            return team;
        }

        public static void InputGameScore(Game game)
        {
            if (game == null || game.IsPlayed || game.FirstTeam == null || game.SecondTeam == null)
                return;

            int minScoreValue = 0;
            game.IsPlayed = true;

            _consoleWorker.WriteLineMessage(Messages.ShowOpponents(game.FirstTeam.Name, game.SecondTeam.Name));

            _consoleWorker.WriteMessage(Messages.SetTeamScore(game.FirstTeam.Name));
            game.FirstTeamScore = InputIntegerNumber(minScoreValue);

            _consoleWorker.WriteMessage(Messages.SetTeamScore(game.SecondTeam.Name));
            game.SecondTeamScore = InputIntegerNumber(minScoreValue);

            _consoleWorker.WriteLineMessage(null);
            game.SetWinner();
        }

        public static void InputTourGamesScores(Tour tour)
        {
            foreach (var game in tour.Games)
            {
                InputGameScore(game);
            }
        }

        public static List<Team> InputTeams()
        {
            int minCountTeams = 2;
            var teams = new List<Team>();
            _consoleWorker.WriteMessage(Messages.InputCountTeams);
            var countTeams = InputIntegerNumber(minCountTeams);

            _consoleWorker.WriteLineMessage(Messages.InputNamesOfTeams);

            for (int i = 0; i < countTeams; i++)
            {
                _consoleWorker.WriteMessage(Messages.SomeIndex(i + 1));
                teams.Add(InputCorrectTeam(teams));
            }

            return teams;
        }
    }
}
