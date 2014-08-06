using System.Collections.Generic;

namespace FilmService.KindsOfGenerators
{
    public abstract class StatementGenerator
    {
        public DataForStatement CurrentData { get; internal set; }

        public StatementGenerator()
        {
            CurrentData=new DataForStatement();
        }

        public void FormDataForStatement(string name, List<Rental> rentals)
        {
            CurrentData.Name = name;
            foreach (var rental in rentals)
            {
                var thisAmount = rental.Movie.CurrentCalculator.Calculate(rental.DaysRented);
                CurrentData.RentalsData[rental.Movie.Title] = thisAmount;
                CurrentData.FrequentRenterPoints += rental.Movie.CurrentCalculator.GetPoints();
                CurrentData.TotalAmount += thisAmount;
            }
        }

        public abstract void Generate(string path);
    }
}
