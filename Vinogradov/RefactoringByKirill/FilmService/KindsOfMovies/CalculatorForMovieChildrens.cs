namespace FilmService.KindsOfMovies
{
    public class CalculatorForMovieChildrens : CalculatorForMovie
    {
        public override double Calculate(int daysRented)
        {
            double result = 0;
            result += 1.5;
            if (daysRented > 3)
                result += (daysRented - 3) * 1.5;
            return result;
        }

        public override int GetPoints()
        {
            return 1;
        }
    }
}
