namespace FilmService.KindsOfMovies
{
    class CalculatorForNewReleaseMovie:CalculatorForMovie
    {
        public override double Calculate(Rental rental)
        {
            result += rental.DaysRented * 3;
            return result;
        }
    }
}
