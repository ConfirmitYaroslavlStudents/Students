using FilmService.KindsOfMovies;

namespace FilmService
{
    //Implement resharper's tips
    public class Movie
    {
        public ICalculatorForMovie CurrentCalculator
        {
            get; 
            set;
        }
        public string Title
        {
            get;
            set;
        }

        public Movie(string title, ICalculatorForMovie currentCalculator)
        {
            CurrentCalculator = currentCalculator;
            Title = title;
        }
    }
}
