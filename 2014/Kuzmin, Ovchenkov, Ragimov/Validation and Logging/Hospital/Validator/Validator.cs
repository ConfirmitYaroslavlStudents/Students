using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Validation
{
    public static class Validator
    {
        private static bool IsCorrectFieldData(string verifyingString, string pattern)
        {
            if (Regex.Match(verifyingString, pattern).Success) return true;

            return false;
        }

        public static bool CheckName(string name)
        {
            const string namePattern = @"^\p{L}+(\s+\p{L}+)*$";

            return IsCorrectFieldData(name, namePattern);
        }
        public static bool CheckDate(string day, string month, string year)
        {
            const string monthPattern = @"(1[0-2]|0[1-9]|\d)";
            const string yearPattern = @"(19|2[0-1])\d{2}$";
            const string dayPattern = @"[0-9]";

            return IsCorrectFieldData(month, monthPattern)
                && IsCorrectFieldData(year, yearPattern)
                && IsCorrectFieldData(day, dayPattern);
        }
        public static bool CheckAddress(string adress)
        {
            return CheckString(adress);
        }
        public static bool CheckPolicyNumber(string policyNumber)
        {
            const string policyNumberPattern = @"[0-9]";

            return IsCorrectFieldData(policyNumber, policyNumberPattern);
        }

        public static bool CheckString(string value)
        {
            return !String.IsNullOrEmpty(value);
        }

        public static bool CheckInt(string value)
        {
            int result;
            if (int.TryParse(value, out result))
            {
                return true;
            }
            return false;
        }
    }
}
