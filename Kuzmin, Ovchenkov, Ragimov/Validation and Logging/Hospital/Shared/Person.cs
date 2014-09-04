using System;

namespace Shared
{
    public class Person
    {
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

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public int Age { get; private set; }
        public string Address { get; private set; }
        public string PolicyNumber { get; private set; }
    }
}