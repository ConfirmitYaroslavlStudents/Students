using System.Collections.Generic;

namespace Shared.Interfaces
{
    public interface IAnalysisDataAccessLayer
    {
        void AddAnalysis(string policyNumber, Analysis analysis);
        List<Analysis> GetAnalyzes(string policyNumber);
    }
}