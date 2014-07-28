using System.IO;
using FilmService;
using FilmService.KindsOfGenerators;
using FilmService.KindsOfMovies;

namespace RefactoringByKirill
{
    class Program
    {
        static void Main(string[] args)
        {
            var user = new Customer("Igor", new StatementGeneratorJSON());
            user.Rentals.Add(new Rental(new Movie("Edge of Tomorrow",new CalculatorForMovieNewRelease()), 5));
            user.Rentals.Add(new Rental(new Movie("Gravity", new CalculatorForMovieRegular()), 2));
            using (var output = new StreamWriter("output.txt"))
            {
                output.Write(user.CurrentStatementGenerator.Generate(user.Name,user.Rentals));
            }
        }
    }
}
