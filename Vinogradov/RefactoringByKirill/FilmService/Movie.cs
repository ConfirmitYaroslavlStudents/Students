namespace FilmService
{
    public class Movie
    {
        public CalculatorForMovie CurrentCalculator { get; set; }

        public Movie(CalculatorForMovie currentCalculator)
        {
            CurrentCalculator = currentCalculator;
        }

        public string Title
        {
            get; 
            set; 
        }
    }
}
