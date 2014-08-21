using System.Collections.Generic;
using HospitalLib.Database;

namespace HospitalLib.Interfaces
{
    public interface IStorage
    {
        string DatabasePath { get; set; }
        IList<Template.Template> Search(Person person, ITemplateProvider templateProvider);
        void Save(Template.Template template, Person person);
    }
}
