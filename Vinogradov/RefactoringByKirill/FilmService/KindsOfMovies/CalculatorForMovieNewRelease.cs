namespace FilmService.KindsOfMovies
{
    public class CalculatorForMovieNewRelease : ICalculatorForMovie
    {
        public double Calculate(int daysRented)
        {
            double result = 0;
            result += daysRented * 3;
            return result;
        }

        public int GetPoints()
        {
            return 2;
        }
    }
}
