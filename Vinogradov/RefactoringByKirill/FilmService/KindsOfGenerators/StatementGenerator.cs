using System.Collections.Generic;

namespace FilmService.KindsOfGenerators
{
    public abstract class StatementGenerator
    {
        public abstract string Generate(string name, List<Rental> rentals);
    }
}
