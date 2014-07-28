namespace FilmService.KindsOfMovies
{
    public class CalculatorForMovieNewRelease : CalculatorForMovie
    {
        public override double Calculate(int daysRented)
        {
            double result = 0;
            result += daysRented * 3;
            return result;
        }

        public override int GetPoints()
        {
            return 2;
        }
    }
}
