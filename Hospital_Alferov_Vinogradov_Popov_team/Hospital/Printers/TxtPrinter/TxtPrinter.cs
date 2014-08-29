using System.IO;
using System.Text;
using Shared;
using Shared.Interfaces;

namespace TxtPrinter
{
    public class TxtPrinter : IPrinter
    {
        public string PathToFile { get; set; }

        public void Print(Person user, Analysis analysis, Template template)
        {
            File.WriteAllText(PathToFile + ".txt", CreateTxtReport(user, analysis, template));
        }

        private string CreateTxtReport(Person user, Analysis analysis, Template template)
        {
            var result = new StringBuilder();
            result.AppendLine(analysis.TemplateTitle);
            result.AppendFormat("Name: {0} {1}\r\n", user.FirstName, user.LastName);
            result.AppendFormat("Insurance policy number: {0}\r\n", user.PolicyNumber);
            result.AppendFormat("Date of birth: {0}\r\n", user.DateOfBirth);
            result.AppendFormat("The number of full years: {0}\r\n", user.Age);
            result.AppendFormat("Live at the address: {0}\r\n\r\n", user.Address);
            result.AppendFormat("Results of the analysis from {0}\r\n", analysis.Date);

            for (int i = 0; i < analysis.Data.Count; ++i)
            {
                result.AppendLine(string.Format("{0} : {1}", template.Data[i], analysis.Data[i]));
            }
            result.AppendLine("\r\n");

            return result.ToString();
        }
    }
}