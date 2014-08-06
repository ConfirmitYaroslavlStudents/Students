namespace Refactoring
{
    public abstract class Price
    {
        public abstract double GetPrice(int daysRented);
        public abstract TypeOfMovie GetPriceCode();

        public virtual int GetFrequentPoints(int daysRented)
        {
            return 1;
        }
    }

    public class RegularPrice : Price
    {
        public override double GetPrice(int daysRented)
        {
            double result = 2;
            if (daysRented > 2)
            {
                result += (daysRented - 2) * 1.5;
            }
            return result;
        }

        public override TypeOfMovie GetPriceCode()
        {
            return TypeOfMovie.Regular;
        }
    }

    public class ChildrensPrice : Price
    {
        public override double GetPrice(int daysRented)
        {
            var result = 1.5;
            if (daysRented > 3)
                result += (daysRented - 3) * 1.5;
            return result;
        }

        public override TypeOfMovie GetPriceCode()
        {
            return TypeOfMovie.Childrens;
        }
    }

    public class NewReleasePrice : Price
    {
        public override double GetPrice(int daysRented)
        {
            return daysRented * 3;
        }

        public override TypeOfMovie GetPriceCode()
        {
            return TypeOfMovie.NewRelease;
        }

        public override int GetFrequentPoints(int daysRented)
        {
            return (daysRented > 1) ? 2 : 1;
        }
    }
}


