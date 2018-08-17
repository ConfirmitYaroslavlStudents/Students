namespace TournamentLibrary
{
    public abstract class Starter
    {
        public const int MinChars = 2;
        public const int MaxChars = 8;

        public abstract Tournament TryLoadTournament();

        public abstract bool ReadDoubleEliminationFlag();

        public abstract int ReadNumberOfPlayers();

        public abstract string[] ReadNames(int numberOfPlayers);

        protected bool NameValidation(string newName, string[] names, int validNames)
        {
            if (newName.Length < MinChars || newName.Length > MaxChars)
            {
                return false;
            }

            foreach (char symbol in newName)
            {
                if (!char.IsLetterOrDigit(symbol))
                {
                    return false;
                }
            }

            for (int i = 0; i < validNames; i++)
            {
                if (names[i] == newName)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
