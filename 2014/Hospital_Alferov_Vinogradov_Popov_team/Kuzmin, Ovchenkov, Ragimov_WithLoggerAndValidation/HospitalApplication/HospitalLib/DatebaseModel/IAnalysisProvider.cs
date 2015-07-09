using System.Collections.Generic;
using HospitalLib.Data;

namespace HospitalLib.DatebaseModel
{
    internal interface IAnalysisProvider
    {
        IList<Analysis> Load(Person person);
        void Save(ref Analysis analysis);
        void Update(Analysis analysis);
        void Remove(Analysis analysis);
        void Remove(Person person);
        void Remove(Template template);
        int GetCount();
    }
}