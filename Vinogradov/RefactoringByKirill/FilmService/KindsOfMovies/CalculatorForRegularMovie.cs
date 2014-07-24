namespace FilmService
{
    class CalculatorForRegularMovie: CalculatorForMovie
    {
        public override double Calculate(Rental rental)
        {
            result += 2;
            if (rental.DaysRented > 2)
            {
                result += (rental.DaysRented - 2) * 1.5;
            }
            return result;
        }
    }
}
