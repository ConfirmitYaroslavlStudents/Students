using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace RefactoringSample
{
    [DataContract]
    public class Statement
    {
        private readonly Customer _customer;

        [DataMember]
        private string _name;
        [DataMember]
        private double _totalAmount;
        [DataMember]
        private int _frequentRenterPoints;

        [DataMember]
        private Dictionary<string, double> _moviePrices;

        public Statement(Customer customer)
        {
            _customer = customer;
            
            _moviePrices = new Dictionary<string, double>();

            _totalAmount = 0;
            _frequentRenterPoints = 0;
            _name = _customer.Name;

            foreach (var rental in _customer.Rentals)
            {
                var thisAmount = rental.GetCharge();

                _frequentRenterPoints += rental.GetFrequentPoints();

                _moviePrices[rental.Movie.Title] = thisAmount;
                _totalAmount += thisAmount;
            }
        }

        public string StandartString()
        {
            var result = String.Format("Учет аренды для {0}{1}", _customer.Name, Environment.NewLine);

            foreach (var movie in _moviePrices.Keys)
            {

                result += String.Format("\t {0} \t {1} {2}", movie, _moviePrices[movie].ToString(CultureInfo.InvariantCulture), Environment.NewLine);
            }

            result += String.Format("Сумма задолженности составляет {0}{1}", _totalAmount.ToString(CultureInfo.InvariantCulture), Environment.NewLine);
            result += String.Format("Вы заработали {0} за активность", _frequentRenterPoints.ToString(CultureInfo.InvariantCulture));
            return result;
        }

        public MemoryStream Json()
        {
            var stream = new MemoryStream();
            var ser = new DataContractJsonSerializer(typeof(Statement));
            ser.WriteObject(stream, this);

            return stream;
        }

        public string JsonToString()
        {
            var stream = Json();

            stream.Position = 0;
            var sr = new StreamReader(stream);

            return sr.ReadToEnd();
        }
    }
}
