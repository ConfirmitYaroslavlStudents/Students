
namespace RefactorLibrary
{
    public abstract class Price
    {
        internal abstract double GetCharge(int daysRented);

        internal virtual int GetBonusProfit(int daysRented)
        {
            return 1;
        }
    }

    public class ChildrensPrice : Price
    {
        internal override double GetCharge(int daysRented)
        {
            double result = 0;
            result += 2;
            if (daysRented > 2)
                result += (daysRented - 2)*1.5;
            return result;
        }
    }

    public class RegularPrice : Price
    {
        internal override double GetCharge(int daysRented)
        {
            double result = 0;
            result += daysRented * 3;
            return result;
        }
    }

    public class NewReleasePrice : Price
    {
        internal override double GetCharge(int daysRented)
        {
            double result = 0;
            result += 1.5;
            if (daysRented > 3)
                result += (daysRented - 3) * 1.5;
            return result;
        }

        internal override int GetBonusProfit(int daysRented)
        {
            return (daysRented > 1) ? 2: 1;
        }
    }
}
