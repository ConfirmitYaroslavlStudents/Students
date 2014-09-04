using System;
using System.Text.RegularExpressions;

namespace HospitalLib.Utils.Validators
{
    public static class Validator
    {
        public static void ValidateNameField(string userInput)
        {
            ValidateField(userInput, @"^[a-zA-Z\p{L}''-'\s]{1,40}$");
        }

        public static void ValidateTitleField(string userInput)
        {
            ValidateField(userInput, @"^[a-zA-Z\d\p{L}''-'\s]{1,40}$");
        }

        public static void ValidateAnalysisField(string userInput)
        {
            ValidateField(userInput, @"^[a-zA-Z.,\d\p{L}''-'\s]{1,40}$");
        }

        private static void ValidateField(string userInput, string pattern)
        {
            if (!Regex.IsMatch(userInput, pattern, RegexOptions.Compiled))
            {
                throw new FormatException(string.Format("Your input data contains prohibited characters!"));
            }
        }
    }
}