using System.Collections.Generic;

namespace Shared.Interfaces
{
    public interface IPersonDataAccessLayer
    {
        List<Person> GetPersons(string firstName, string lastName, string policyNumber);
        bool AddPerson(Person person);
    }
}