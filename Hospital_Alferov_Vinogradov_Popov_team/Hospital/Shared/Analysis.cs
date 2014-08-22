using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public class Analysis
    {
        public string TemplateTitle { get; private set; }
        public Dictionary<string, string> Data { get; private set; }
        public DateTime Date { get; private set; }

        public Analysis(Template filledTemplate, DateTime date)
        {
            Date = date;
            TemplateTitle = filledTemplate.Title;
            Data = filledTemplate.Data;
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            result.AppendLine(Date.ToString());
            foreach (var item in Data)
            {
                result.AppendLine(string.Format("{0} : {1}", item.Key, item.Value));
            }
            result.AppendLine("\n");
            return result.ToString();
        }
    }
}
