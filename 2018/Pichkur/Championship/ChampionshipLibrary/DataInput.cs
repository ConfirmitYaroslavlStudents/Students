using System.Collections.Generic;

namespace ChampionshipLibrary
{
    public class DataInput
    {
        private IDataInputWorker _dataReader;

        public DataInput(IDataInputWorker worker)
        {
            _dataReader = worker;
        }       

        public int InputIntegerNumber(int minValue)
        {
            var number = -1;

            while (!int.TryParse(_dataReader.InputString(), out number) || number < minValue)
            {
                _dataReader.WriteLineMessage(Messages.NotAPositiveIntegerNumber);
                _dataReader.WriteMessage(Messages.TryAgain);
            }

            return number;
        }

        public Team InputTeam(List<Team> teams)
        {
            var team = new Team(_dataReader.InputTeamName());

            while (teams.Contains(team))
            {
                _dataReader.WriteLineMessage(Messages.ImpossibleTeamName);
                _dataReader.WriteMessage(Messages.TryAgain);
                team = new Team(_dataReader.InputTeamName());
            }

            return team;
        }

        public void InputGameScore(Game game)
        {
            if (game == null || game.IsPlayed || game.FirstTeam == null || game.SecondTeam == null)
                return;

            int minScoreValue = 0;
            game.IsPlayed = true;

            _dataReader.WriteLineMessage(Messages.ShowOpponents(game.FirstTeam.Name, game.SecondTeam.Name));

            _dataReader.WriteMessage(Messages.SetTeamScore(game.FirstTeam.Name));
            game.FirstTeamScore = InputIntegerNumber(minScoreValue);

            _dataReader.WriteMessage(Messages.SetTeamScore(game.SecondTeam.Name));
            game.SecondTeamScore = InputIntegerNumber(minScoreValue);

            _dataReader.WriteLineMessage(null);
            game.SetWinner();
        }

        public void InputTourGamesScores(Tour tour)
        {
            foreach (var game in tour.Games)
            {
                InputGameScore(game);
            }
        }

        public List<Team> InputTeams()
        {
            int minCountTeams = 2;
            var teams = new List<Team>();
            _dataReader.WriteMessage(Messages.InputCountTeams);
            var countTeams = InputIntegerNumber(minCountTeams);

            _dataReader.WriteLineMessage(Messages.InputNamesOfTeams);

            for (int i = 0; i < countTeams; i++)
            {
                _dataReader.WriteMessage(Messages.SomeIndex(i + 1));
                teams.Add(InputTeam(teams));
            }

            return teams;
        }
    }
}
