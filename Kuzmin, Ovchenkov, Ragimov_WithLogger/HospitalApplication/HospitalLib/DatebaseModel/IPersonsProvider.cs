using System.Collections.Generic;
using HospitalLib.Data;

namespace HospitalLib.DatebaseModel
{
    public interface IPersonsProvider
    {
        IList<Person> Load();
        void Save(ref Person person);
        void Update(Person person);
        void Remove(Person person);
        int GetCount();
        IList<Person> Search(string firstName, string lastName);
    }
}
