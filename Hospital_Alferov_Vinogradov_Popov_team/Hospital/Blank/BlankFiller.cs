using System;
using System.Globalization;
using System.IO;
using System.Text;
using Shared;

namespace Blank
{
    public class BlankFiller
    {
        private string _pathToHtmlBlank;

        private const string filedFirstName = "FirstName";
        private const string filedLastName = "LastName";
        private const string filedDateOfBirth = "DateOfBirth";
        private const string filedAge = "Age";
        private const string filedAddress = "Address";
        private const string filedPolicyNumber = "PolicyNumber";
        private const string filedDateAnalyse = "DateAnalyse";
        private const string filedTitleAnalyse = "TitleAnalyse";
        

        private const string fieldValue = "Value";

        public BlankFiller(string pathToHtmlBlank)
        {
            _pathToHtmlBlank = pathToHtmlBlank;
        }

        public string FillBlank(Person user, Analysis analysis, Template template)
        {
            StringBuilder text = GetText();

            text.ReplaceWithExeption(filedFirstName + fieldValue, user.FirstName);
            text.ReplaceWithExeption(filedLastName + fieldValue, user.LastName);
            text.ReplaceWithExeption(filedDateOfBirth + fieldValue, user.DateOfBirth.ToShortDateString());
            text.ReplaceWithExeption(filedAge + fieldValue, user.Age.ToString(CultureInfo.InvariantCulture));
            text.ReplaceWithExeption(filedAddress + fieldValue, user.Address);
            text.ReplaceWithExeption(filedPolicyNumber + fieldValue, user.PolicyNumber);
            text.ReplaceWithExeption(filedDateAnalyse + fieldValue, analysis.Date.ToShortDateString());
            text.ReplaceWithExeption(filedTitleAnalyse + fieldValue, analysis.TemplateTitle);
            
            
            for(int i = 0; i < analysis.Data.Count; ++i)
            {
                var placeToTeplace = template.Data[i].Trim() + fieldValue;
                text.ReplaceWithExeption(placeToTeplace, analysis.Data[i]);
            }
            return text.ToString();
        }

        private StringBuilder GetText()
        {
            return new StringBuilder(File.ReadAllText(_pathToHtmlBlank));
        }

        
    }

    public static class ExtensionClass
    {
        public static void ReplaceWithExeption(this StringBuilder text, string oldValue, string newValue)
        {
            if (ExistSubstring(text.ToString(), oldValue))
            {
                text.Replace(oldValue, newValue);
            }
            else
            {
                throw new InvalidOperationException("Field is not found " + oldValue);
            }
        }
        private static  bool ExistSubstring(string text, string substring)
        {
            return text.IndexOf(substring) != -1;
        }
    }

    

}
