using System.IO;
using System.Text;
using Shared;
using Shared.Interfaces;

namespace TxtPrinter
{
    public class TxtPrinter : IPrinter
    {
        public void Print(Person user, Analysis analysis, string pathToFile)
        {
            File.WriteAllText(pathToFile + ".txt", CreateTxtReport(user, analysis));
        }

        private string CreateTxtReport(Person user, Analysis analysis)
        {
            var result = new StringBuilder();
            result.Append(analysis.TemplateTitle);
            result.AppendLine();
            result.AppendLine();
            result.AppendFormat("Name: {0} {1}", user.FirstName, user.LastName);
            result.AppendLine();
            result.AppendFormat("Insurance policy number: {0}", user.PolicyNumber);
            result.AppendLine();
            result.AppendFormat("Date of birth: {0}", user.DateOfBirth);
            result.AppendLine();
            result.AppendFormat("The number of full years: {0}", user.Age);
            result.AppendLine();
            result.AppendFormat("Live at the address: {0}", user.Address);
            result.AppendLine();

            result.AppendLine();
            result.AppendFormat("Results of the analysis from {0}", analysis.Date);

            foreach (var analysisRecord in analysis.Data)
            {
                result.AppendLine();
                result.AppendFormat("{0}: {1}", analysisRecord.Key, analysisRecord.Value);
            }

            return result.ToString();
        }
    }
}
