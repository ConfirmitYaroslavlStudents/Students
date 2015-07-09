using System;
using System.Collections.Generic;


namespace RomeDigitLibrary
{
    public static class RulesConvertRomeNumber
    {
        public static Dictionary<uint, string> RulesConvertToRome { get; private set; }
        
        public static Dictionary<char, uint> RulesConvertToArab { get; private set; } 

        static RulesConvertRomeNumber()
        {
            RulesConvertToRome = new Dictionary<uint, string> 
            { 
            { 1, "I" },{ 4, "IV"},{ 5, "V" },
            { 9, "IX"},{10, "X"},{40, "XL"},
            {50, "L"},{90, "XC"},{100, "C"},
            {400, "CD"},{500, "D"},{900, "CM"},
            {1000, "M"}
            };
            
            RulesConvertToArab = new Dictionary<char, uint>
            {
                {'I', 1},{'V', 5},{'X', 10},
                {'L', 50},{'C', 100},{'D', 500},
                {'M', 1000}
            };
        }

       
        public static void CheckExistRule(string rule)
        {
            if (!RulesConvertToRome.ContainsValue(rule))
                throw new FormatException("Incorrect entry of the Roman number");
        }
        public static void CheckExistRule(char rule)
        {
            if (!RulesConvertToArab.ContainsKey(rule))
                throw new FormatException("Incorrect entry of the Roman number");
        }
    }
}
