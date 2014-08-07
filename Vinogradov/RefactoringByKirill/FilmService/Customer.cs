using System.Collections.Generic;
using FilmService.KindsOfGenerators;

namespace FilmService
{
    public class Customer
    {
        //Change the format of properties(code should be formatted), make setters private as possible
        public StatementGenerator CurrentStatementGenerator
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public List<Rental> Rentals
        {
            get;
            private set;
        }

        public Customer(string name, StatementGenerator currentStatementGenerator)
        {
            Name = name;
            Rentals = new List<Rental>();
            CurrentStatementGenerator = currentStatementGenerator;
        }
    }
}