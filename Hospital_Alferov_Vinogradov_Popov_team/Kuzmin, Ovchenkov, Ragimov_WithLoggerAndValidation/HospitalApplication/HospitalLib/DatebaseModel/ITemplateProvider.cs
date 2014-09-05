using System.Collections.Generic;
using HospitalLib.Data;

namespace HospitalLib.DatebaseModel
{
    public interface ITemplateProvider
    {
        IList<Template> Load();
        IList<Template> Search(string tempalteName);
        void Save(Template template);
        void Update(Template template);
        void Remove(Template template);
        int GetCount();
    }
}