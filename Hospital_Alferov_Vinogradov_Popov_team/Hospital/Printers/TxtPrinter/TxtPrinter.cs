using System.IO;
using System.Text;
using Shared;

namespace TxtPrinter
{
    public class TxtPrinter : IPrinter
    {
        private readonly string _pathToFile;

        public TxtPrinter(string pathToFile)
        {
            _pathToFile = pathToFile;
        }

        public void Print(Person user, Analysis analysis, Template template)
        {
            File.WriteAllText(_pathToFile + ".txt", CreateTxtReport(user, analysis, template));
        }

        private string CreateTxtReport(Person user, Analysis analysis, Template template)
        {
            var result = new StringBuilder();
            result.AppendLine(analysis.TemplateTitle);
            result.AppendFormat("Name: {0} {1}\n", user.FirstName, user.LastName);
            result.AppendFormat("Insurance policy number: {0}\n", user.PolicyNumber);
            result.AppendFormat("Date of birth: {0}\n", user.DateOfBirth);
            result.AppendFormat("The number of full years: {0}\n", user.Age);
            result.AppendFormat("Live at the address: {0}\n\n", user.Address);
            result.AppendFormat("Results of the analysis from {0}", analysis.Date);

            for (int i = 0; i < analysis.Data.Count; ++i)
            {
                result.AppendLine(string.Format("{0} : {1}", template.Data[i], analysis.Data[i]));
            }
            result.AppendLine("\n");

            return result.ToString();
        }
    }
}
