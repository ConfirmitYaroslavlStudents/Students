namespace RefactoringDemo.Steps.Step3
{
    public class Rental
    {
        public Movie Movie
        {
            get; 
            set;
        }

        public int DaysRented
        {
            get; 
            set;
        }

        public double GetCharge()
        {
            double result = 0;

            switch (Movie.PriceCode)
            {
                case Movie.REGULAR:
                    result += 2;
                    if (DaysRented > 2)
                        result += (DaysRented - 2) * 1.5;
                    break;

                case Movie.NEW_RELEASE:
                    result += DaysRented * 3;
                    break;

                case Movie.CHILDRENS:
                    result += 1.5;
                    if (DaysRented > 3)
                        result += (DaysRented - 3) * 1.5;
                    break;
            }
            return result;
        }
    }
}