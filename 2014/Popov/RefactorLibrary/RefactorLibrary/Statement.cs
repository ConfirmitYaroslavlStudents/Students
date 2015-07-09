using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace RefactorLibrary
{
    [DataContract]
    class TemplateTitleCharge : IEquatable<TemplateTitleCharge>
    {
        [DataMember]
        public string Title { get; private set; }
        [DataMember]
        public double Charge { get; private set; }

        public TemplateTitleCharge(Rental rent)
        {
            Title = rent.Movie.Title;
            Charge = rent.GetCharge();
        }
        

        public override bool Equals(object obj)
        {
            if (obj is TemplateTitleCharge)
            {
                return Equals(obj as TemplateTitleCharge);
            }
            return false;
        }

        public bool Equals(TemplateTitleCharge other)
        {
            return ((Title == other.Title) && (Charge == other.Charge));
        }
    }

    [DataContract]
    public class Statement : IEquatable<Statement>
    {
        [DataMember]
        private string _nameClient;
        [DataMember]
        private double _debtAmount;
        [DataMember]
        private double _earnings;
        [DataMember]
        private List<TemplateTitleCharge> _listTitleCharge;


        public Statement(Customer client)
        {
            _nameClient = client.Name;
            _debtAmount = client.GetTotalCharge();
            _earnings = client.GetBonusProfit();
            _listTitleCharge = new List<TemplateTitleCharge>();
            foreach (var rent in client.Rentals)
            {
                _listTitleCharge.Add(new TemplateTitleCharge(rent));
            }
        }
       

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public string ToSimpleString()
        {
            var result = "Учет аренды для " + _nameClient + "\n";
            foreach (var template in _listTitleCharge)
            {
                result += "\t" + template.Title + "\t" + template.Charge + "\n";
            }
            result += "Сумма задолженности составляет " + _debtAmount + "\n";
            result += "Вы заработали " + _earnings + " за активность";
            return result;
        }
        

        public override bool Equals(object obj)
        {
            if (obj is Statement)
            {
                return Equals(obj as Statement);
            }
            return false;
        }

        public bool Equals(Statement other)
        {
            return AllFieldsEquals(other);
        }

        private bool AllFieldsEquals(Statement other)
        {
            return (
                (_nameClient == other._nameClient) &&
                (_debtAmount == other._debtAmount) &&
                (_earnings == other._earnings) &&
                (_listTitleCharge.SequenceEqual(other._listTitleCharge))
                );
        }
    }
}
