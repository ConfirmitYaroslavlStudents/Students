using System.Collections.Generic;

namespace Shared
{
    public interface IDataStorage
    {
        List<Person> GetPersons(string fieldName, string fieldValue);
        List<Template> GetTemplates();
        Template GetTemplate(string title);
        bool AddPerson(Person person);
        bool AddTemplate(Template template);
        void AddAnalysis(string policyNumber, Analysis analysis);
        List<Analysis> GetAnalyzes(string policyNumber);
    }
}
