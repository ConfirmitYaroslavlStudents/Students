using System.Collections.Generic;

namespace Shared
{
    public interface IHospitalDAL
    {
        List<Person> GetPersons(string firstName, string lastName, string policyNumber);
        List<Template> GetTemplates();
        Template GetTemplate(string title);
        bool AddPerson(Person person);
        bool AddTemplate(Template template);
        void AddAnalysis(string policyNumber, Analysis analysis);
        List<Analysis> GetAnalyzes(string policyNumber);
    }
}
