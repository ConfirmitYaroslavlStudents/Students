namespace RefactoringSample
{
    public abstract class PriceType
    {
        public virtual double GetPrice(int daysRented)
        {
            double result = 2;
            if (daysRented > 2)
                result += (daysRented - 2) * 1.5;

            return result;
        }

        public virtual int GetFrequentPoints(int daysRented)
        {
            return 1;
        }
    }

    public class PriceRegular : PriceType
    {
    }

    public class PriceNewRelease : PriceType
    {
        public override double GetPrice(int daysRented)
        {
            return daysRented * 3;
        }
    }

    public class PriceChildren : PriceType
    {
        public override double GetPrice(int daysRented)
        {
            if (daysRented > 3)
                return 1.5 + (daysRented - 3) * 1.5;
            return 1.5;
        }

        public override int GetFrequentPoints(int daysRented)
        {
            return daysRented > 1 ? 2 : 1;
        }
    }

}
