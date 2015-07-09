using FilmService.KindsOfMovies;

namespace FilmService
{
    public class Movie
    {
        public ICalculator CurrentCalculator { get; set; }
        public string Title { get; set; }

        public Movie(string title, ICalculator currentCalculator)
        {
            CurrentCalculator = currentCalculator;
            Title = title;
        }
    }
}
