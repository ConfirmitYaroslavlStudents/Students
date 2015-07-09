using System.Collections.Generic;
using FilmService.KindsOfStatements;

namespace FilmService
{
    public class Customer
    {
        public DataStore CurrentDataStore { get; private set; }
        public Statement StatementGenerator { get; private set; }
        public string Name { get; set; }
        public List<Rental> Rentals { get; private set; }

        public Customer(string name, Statement statementGenerator)
        {
            Name = name;
            Rentals = new List<Rental>();
            StatementGenerator = statementGenerator;
            CurrentDataStore = new DataStore();
        }

        public void RequestAndSetDataStore()
        {
            CurrentDataStore.FormDataStore(Name,Rentals);
        }
    }
}