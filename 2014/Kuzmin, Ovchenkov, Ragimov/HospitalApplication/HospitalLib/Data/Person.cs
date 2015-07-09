using System;
using System.Globalization;
using System.Runtime.Serialization;
using HospitalLib.Providers;

namespace HospitalLib.Data
{
    [DataContract]
    public class Person
    {
        [DataMember] private DateTime _birthDate;
        [DataMember] public int Id { get; private set; }
        [DataMember] public string FirstName { get; set; }
        [DataMember] public string LastName { get; set; }
        [DataMember] public string MiddleName { get; set; }

        public DateTime BirthDate
        {
            get { return _birthDate; }
            set
            {
                if (value.CompareTo(DateTime.Now) > -1)
                    throw new ArgumentException("BirthDate! We don't serve time travelers");

                _birthDate = value;
            }
        }

        public Person(string firstName, string lastName, string middleName, DateTime birthDate)
        {
            LastName = lastName;
            FirstName = firstName;
            MiddleName = middleName;
            BirthDate = birthDate;
        }

        internal Person(int id, string firstName, string lastName, string middleName, DateTime birthDate)
            : this(firstName, lastName, middleName, birthDate)
        {
            Id = id;
        }

        public override string ToString()
        {
            return "#" + (Id + 1).ToString(CultureInfo.InvariantCulture) + " " + LastName + " " + FirstName + " "
                   + MiddleName + " " + BirthDate.ToShortDateString();
        }
    }
}
