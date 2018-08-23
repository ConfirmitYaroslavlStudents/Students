namespace ChampionshipLibrary
{
    public static class Messages
    {
        public static readonly string NotAPositiveIntegerNumber = "You should enter a positive integer number";
        public static readonly string ImpossibleTeamName = "This name is already used";
        public static readonly string TryAgain = "Try again:";
        public static readonly string ChampionshipIsEnd = "Championship is already ended";
        public static readonly string NotAInputTeams = "First you should input teams!";
        public static readonly string InputCountTeams = "Input count of teams:";
        public static readonly string InputNamesOfTeams = "Input teams:";
        public static readonly string SelectStandartGridType = "You choose Standart type of grid";
        public static readonly string SelectDoubleGridType = "You choose Double type of grid";
        public static readonly string LoadError = "Nothing to load";
        public static readonly string TeamsInputAlready = "Championship is already start";

        public static string SomeIndex(int index)
        {
            return $"{index}. ";
        }

        public static string ShowOpponents(string firstOpponent,string secondOpponent)
        {
            return $"{firstOpponent} vs {secondOpponent}";
        }

        public static string SetTeamScore(string team)
        {
            return $"Enter {team} score:";
        }

        public static string ShowChampion(Team champion)
        {
            return $"The \"{champion.Name}\" is Champion!!";
        }
    }
}
