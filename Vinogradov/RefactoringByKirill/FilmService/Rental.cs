using FilmService.KindsOfMovies;

namespace FilmService
{
    public class Rental
    {
        public Movie Movie
        {
            get;
            set;
        }

        public int DaysRented
        {
            get;
            set;
        }

        public int GetPoints()
        {
            var addedPoints = 1;
            if (Movie.CurrentCalculator is CalculatorForNewReleaseMovie && DaysRented > 1)
            {
                addedPoints++;
            }
            return addedPoints;
        }
    }
}