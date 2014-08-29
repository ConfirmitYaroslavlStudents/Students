using System;
using System.Collections.Generic;
using System.Linq;

namespace Shared
{
    public static class AnalysisFactory
    {
        public static Analysis GetAnalysis(IEnumerable<string> analysisData, Template template, DateTime date)
        {
            if (template.Data.Count != analysisData.Count())
            {
                throw new ArgumentException("analysisData");
            }

            return new Analysis(analysisData.ToList(), template.Title, date);
        }
    }
}