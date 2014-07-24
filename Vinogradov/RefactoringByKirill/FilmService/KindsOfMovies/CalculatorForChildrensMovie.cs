namespace FilmService.KindsOfMovies
{
    class CalculatorForChildrensMovie:CalculatorForMovie
    {
        public override double Calculate(int daysRented)
        {
            result += 1.5;
            if (daysRented > 3)
                result += (daysRented - 3) * 1.5;
            return result;
        }
    }
}
