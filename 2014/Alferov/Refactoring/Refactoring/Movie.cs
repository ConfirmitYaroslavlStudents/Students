using System.Runtime.Serialization;

namespace Refactoring
{
    [DataContract]
    public abstract class Movie
    {
        [DataMember]
        public string Title { get; private set; }

        protected Movie(string title)
        {
            Title = title;
        }

        protected Movie() {}

        public abstract double GetPrice(int daysRented);
    }

    [DataContract]
    public class RegularMovie : Movie
    {
        public RegularMovie(string title) : base(title)
        {
        }

        public override double GetPrice(int daysRented)
        {
            double result = 2;

            if (daysRented > 2)
            {
                result += (daysRented - 2)*1.5;
            }

            return result;
        }
    }

    [DataContract]
    public class ChildrensMovie : Movie
    {
        public ChildrensMovie(string title)
            : base(title)
        {
        }

        public override double GetPrice(int daysRented)
        {
            double result = 1.5;

            if (daysRented > 3)
            {
                result += (daysRented - 3)*1.5;
            }

            return result;
        }
    }

    [DataContract]
    public class NewReleaseMovie : Movie
    {
        public NewReleaseMovie(string title)
            : base(title)
        {
        }

        public override double GetPrice(int daysRented)
        {
            return daysRented*3;
        }
    }
}