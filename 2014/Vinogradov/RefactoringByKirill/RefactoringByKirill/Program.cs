using System;
using FilmService;
using FilmService.KindsOfMovies;
using UnitTestsForFilmService;


namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var user = new Customer("Igor", StatementFactory.GetStatement());
            user.Rentals.Add(new Rental(new Movie("Edge of Tomorrow", new NewRelease()), 5));
            user.Rentals.Add(new Rental(new Movie("Gravity", new Regular()), 7));
            user.Rentals.Add(new Rental(new Movie("HOW TO TRAIN YOUR DRAGON 2", new Childrens()), 3));
            user.RequestAndSetDataStore();
            const string path = "userSerialize";

            var serializeData = user.CurrentDataStore;

            user.StatementGenerator.Serialize(path, user.CurrentDataStore);

            var deserializeData = user.StatementGenerator.Deserialize(path);

            if (serializeData.Equals(deserializeData))
            {
                Console.Write("eqels");
            }
            else
            {
                Console.Write("Noooooooooooooooooooo");
            }
            Console.ReadLine();
        }
    }
}
