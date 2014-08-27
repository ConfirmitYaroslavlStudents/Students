using System.Collections.Generic;
using HospitalLib.Data;

namespace HospitalLib.DatebaseModel
{
    public interface ITemplateProvider
    {
        Template Load(string path);
        IList<Template> Load();
        IList<Template> Seach(string search);
        void Save(Template template);
        void Update(Template template);
        void Remove(Template template);
        int GetCount();
    }
}
