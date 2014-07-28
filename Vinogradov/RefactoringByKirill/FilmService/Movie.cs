using FilmService.KindsOfMovies;

namespace FilmService
{
    public class Movie
    {
        public CalculatorForMovie CurrentCalculator
        {
            get; 
            set;
        }
        public string Title
        {
            get;
            set;
        }

        public Movie(string title, CalculatorForMovie currentCalculator)
        {
            CurrentCalculator = currentCalculator;
            Title = title;
        }
    }
}
