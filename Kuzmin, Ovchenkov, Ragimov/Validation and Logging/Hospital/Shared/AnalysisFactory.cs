using System;
using System.Collections.Generic;
using System.Linq;
using LogService;

namespace Shared
{
    public static class AnalysisFactory
    {
        public static Analysis GetAnalysis(IEnumerable<string> analysisData, Template template, DateTime date)
        {
            if (template.Data.Count != analysisData.Count())
            {
                //LOGGING
                Logger.Warn("Analyzes were not obtained");
                throw new ArgumentException("analysisData");
            }

            //LOGGING
            Logger.Info("Analyzes were obtained");
            return new Analysis(analysisData.ToList(), template.Title, date);
        }
    }
}