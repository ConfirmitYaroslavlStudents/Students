using System.Collections.Generic;

namespace TournamentLibrary
{
    public static class NameValidator
    {
        public const int MinChars = 2;
        public const int MaxChars = 8;
        
        public static bool Validate(string newName, List<string> names)
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

            foreach (var name in names)
            {
                if (name == newName)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
