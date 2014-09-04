using System;
using System.Text;
using System.Text.RegularExpressions;

namespace HospitalLib.Utils.Validators
{
    public static class Validator
    {
        public static void ValidateNameField(string userInput)
        {
            ValidateField(userInput, @"[\W+\d+]");
        }

        public static void ValidateTitleField(string userInput)
        {
            ValidateField(userInput, @"[\W+]");
        }

        private static void ValidateField(string userInput, string pattern)
        {
            var result = Regex.Matches(userInput, pattern, RegexOptions.Compiled);
            if (result.Count == 0) return;
            var sb = new StringBuilder();
            foreach (var item in result)
            {
                sb.Append(item);
            }
            throw new FormatException(string.Format("Your input data contains prohibited characters: {0}", sb));
        }
    }
}
