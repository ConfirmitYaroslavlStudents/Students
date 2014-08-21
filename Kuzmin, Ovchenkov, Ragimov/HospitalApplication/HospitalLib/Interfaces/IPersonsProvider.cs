using System.Collections.Generic;
using HospitalLib.Database;

namespace HospitalLib.Interfaces
{
    public interface IPersonsProvider
    {
        string DatabasePath { get; set; }
        IEnumerable<Person> Load();
        void Save(Person person);
    }
}
