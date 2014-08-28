using System;
using System.Collections.Generic;

namespace Shared
{
    [Serializable]
    public class Analysis
    {
        public Analysis(IList<string> analysisData, string templateTitle, DateTime date)
        {
            //if (template.Data.Count != analysisData.Count)
            //{
            //    throw new ArgumentException("analysisData");
            //}

            Date = date;
            TemplateTitle = templateTitle;
            Data = analysisData;
        }

        public string TemplateTitle { get; private set; }
        public IList<string> Data { get; private set; }
        public DateTime Date { get; private set; }
    }
}
