namespace FilmService.KindsOfMovies
{
    public interface ICalculatorForMovie
    {
        double Calculate(int daysRented);
        int GetPoints();
    }
}
