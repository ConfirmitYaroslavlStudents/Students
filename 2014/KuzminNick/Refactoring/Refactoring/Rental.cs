using System.Runtime.Serialization;

namespace VideoService
{
    [DataContract]
    [KnownTypeAttribute(typeof(RentalForChildrenMovie))]
    [KnownTypeAttribute(typeof(RentalForNewReleaseMovie))]
    [KnownTypeAttribute(typeof(RentalForRegularMovie))]
    public abstract class Rental
    {
        private int _daysRented;

        [DataMember]
        public Movie Movie
        {
            get; set;
        }

        public int DaysRented
        {
            get { return _daysRented; }
            set
            {
                if(value > 0)
                    _daysRented = value;
            }
        }

        public abstract double GetRental();

        public virtual int GetFrequentPoints()
        {
            const int frequentRenterPoints = 1;
            return frequentRenterPoints;
        }
    }

    [DataContract]
    public sealed class RentalForChildrenMovie : Rental
    {
        public RentalForChildrenMovie(int daysRented)
        {
            DaysRented = daysRented;
        }

        public override double GetRental()
        {
            var result = 1.5;
            if (DaysRented > 3)
                result += (DaysRented - 3)*1.5;
            return result;
        }
    }

    [DataContract]
    public sealed class RentalForNewReleaseMovie : Rental
    {
        public RentalForNewReleaseMovie(int daysRented)
        {
            DaysRented = daysRented;
        }

        public override int GetFrequentPoints()
        {
            var frequentRenterPoints = base.GetFrequentPoints();
            if (IsNewReleaseMovieRentedMoreOneDay())
                frequentRenterPoints++;
            return frequentRenterPoints;
        }

        private bool IsNewReleaseMovieRentedMoreOneDay()
        {
            return DaysRented > 1;
        }

        public override double GetRental()
        {
            var result = DaysRented * 3;
            return result;
        }
    }

    [DataContract]
    public sealed class RentalForRegularMovie : Rental
    {
        public RentalForRegularMovie(int daysRented)
        {
            DaysRented = daysRented;
        }

        public override double GetRental()
        {
            var result = 2.0;
            if (DaysRented > 2)
                result += (DaysRented - 2) * 1.5;
            return result;
        }
    }
}
