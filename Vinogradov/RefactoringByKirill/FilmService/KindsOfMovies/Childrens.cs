namespace FilmService.KindsOfMovies
{
    public class Childrens : ICalculator
    {
        public double Calculate(int daysRented)
        {
            double result = 0;
            result += 1.5;
            if (daysRented > 3)
                result += (daysRented - 3) * 1.5;
            return result;
        }

        public int GetPoints()
        {
            return 1;
        }
    }
}
