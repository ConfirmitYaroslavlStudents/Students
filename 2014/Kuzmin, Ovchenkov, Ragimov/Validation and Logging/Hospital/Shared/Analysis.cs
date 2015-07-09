using System;
using System.Collections.Generic;
using LogService;

namespace Shared
{
    [Serializable]
    public class Analysis
    {
        public Analysis(IList<string> analysisData, string templateTitle, DateTime date)
        {
            Date = date;
            TemplateTitle = templateTitle;
            Data = analysisData;
            //LOGGING
            Logger.Info("Analysis was created");
        }

        public string TemplateTitle { get; private set; }
        public IList<string> Data { get; private set; }
        public DateTime Date { get; private set; }
    }
}