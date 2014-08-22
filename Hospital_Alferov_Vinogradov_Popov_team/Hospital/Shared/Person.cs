using System;

namespace Shared
{
    public class Person
    {
        private int _age;

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string Address { get; private set; }

        public int Age
        {
            get
            {
                return _age;
            }

            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("age");
                }

                _age = value;
            }
        }

        public string PolicyNumber { get; private set; }
        
        public Person(string firstName, string lastName, DateTime dateOfBirth,
            string address, string policyNumber)
        {
            PolicyNumber = policyNumber;
            Age = DateTime.Now.Year - dateOfBirth.Year;
            Address = address;
            DateOfBirth = dateOfBirth;
            LastName = lastName;
            FirstName = firstName;
        }
    }
}
