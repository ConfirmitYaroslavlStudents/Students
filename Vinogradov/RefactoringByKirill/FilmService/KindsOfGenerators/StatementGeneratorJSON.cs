using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace FilmService.KindsOfGenerators
{
    public class StatementGeneratorJSON : StatementGenerator
    {
        private StringBuilder tabString;

        public StatementGeneratorJSON()
        {
            tabString = new StringBuilder('\n'.ToString(CultureInfo.InvariantCulture));
        }
        public override string Generate(string name, List<Rental> rentals)
        {
            double totalAmount = 0;
            var frequentRenterPoints = 0;
            ChangeTab(true);
            var result = new StringBuilder("{" + GetTab() + "\"Customer\":" + GetTab() + "{");
            ChangeTab(true);
            result.Append(GetTab() + "\"Name\": " + "\"" + name + "\"" + "," + GetTab());
            result.Append("\"Rentals\":" + GetTab() + "[");
            ChangeTab(true);
            for (int i = 0; i < rentals.Count; i++)
            {
                var thisAmount = rentals[i].Movie.CurrentCalculator.Calculate(rentals[i].DaysRented);
                frequentRenterPoints += rentals[i].Movie.CurrentCalculator.GetPoints();
                result.Append(GetTab() + "{");
                ChangeTab(true);
                result.Append(GetTab() + "\"Title\": " + "\"" + rentals[i].Movie.Title + "\"," + GetTab());
                result.Append("\"Amount\": " + thisAmount);
                ChangeTab(false);
                result.Append(GetTab() + "}");
                if (i != rentals.Count - 1)
                {
                    result.Append(",");
                }
                totalAmount += thisAmount;
            }
            ChangeTab(false);
            result.Append(GetTab() + "],"+GetTab());
            result.Append("\"TotalAmount\": " + totalAmount+","+GetTab());
            result.Append("\"FrequentRenterPoints\": "+ frequentRenterPoints);
            ChangeTab(false);
            result.Append(GetTab() + "}");
            ChangeTab(false);
            result.Append(GetTab() + "}");
            return result.ToString();
        }

        private string GetTab()
        {
            return tabString.ToString();
        }
        private void ChangeTab(bool increase)
        {
            if (increase)
            {
                tabString.Append('\t'.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                if (tabString.Length > 1)
                {
                    tabString.Remove(tabString.Length - 1, 1);
                }
            }
        }
    }
}
