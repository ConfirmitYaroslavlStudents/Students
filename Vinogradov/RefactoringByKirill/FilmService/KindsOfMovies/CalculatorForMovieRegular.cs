namespace FilmService.KindsOfMovies
{
    public class CalculatorForMovieRegular : ICalculatorForMovie
    {
        public double Calculate(int daysRented)
        {
            double result = 0;
            result += 2;
            if (daysRented > 2)
            {
                result += (daysRented - 2) * 1.5;
            }
            return result;
        }

        public int GetPoints()
        {
            return 1;
        }
    }
}
