using System;
using System.Globalization;
using System.Runtime.Serialization;
using HospitalLib.Utils.Validators;

namespace HospitalLib.Data
{
    [DataContract]
    public class Person
    {
        [DataMember] private DateTime _birthDate;
        [DataMember] private string _firstName;
        [DataMember] private string _lastName;
        [DataMember] private string _middleName;

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

        [DataMember]
        public int Id { get; private set; }

        [DataMember]
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                Validator.ValidateNameField(value);
                _firstName = value;
            }
        }

        [DataMember]
        public string LastName
        {
            get { return _lastName; }
            set
            {
                Validator.ValidateNameField(value);
                _lastName = value;
            }
        }

        [DataMember]
        public string MiddleName
        {
            get { return _middleName; }
            set
            {
                Validator.ValidateNameField(value);
                _middleName = value;
            }
        }

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

        public override string ToString()
        {
            return "#" + (Id + 1).ToString(CultureInfo.InvariantCulture) + " " + LastName + " " + FirstName + " "
                   + MiddleName + " " + BirthDate.ToShortDateString();
        }
    }
}