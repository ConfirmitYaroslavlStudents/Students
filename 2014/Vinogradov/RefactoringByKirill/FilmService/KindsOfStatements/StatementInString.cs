using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FilmService.KindsOfStatements
{
    public class StatementInString : Statement
    {
        public StatementInString()
        {
            postfix = ".string";
        }
        public override void Serialize(string path, DataStore currentData)
        {
            var result = "Учет аренды для " + currentData.Name + "\n";
            foreach (var item in currentData.RentalsData)
            {
                result += "\t" + item.Key + "\t" + item.Value + "\n";
            }
            result += "Сумма задолженности составляет " + currentData.TotalAmount + "\n";
            result += "Вы заработали " + currentData.FrequentRenterPoints + " за активность";
            using (var output = new StreamWriter(path + postfix, false))
            {
                output.Write(result);
            }
        }

        public override DataStore Deserialize(string path)
        {
            string name;
            var rentalsData = new Dictionary<string, double>();
            double totalAmount;
            int frequentRenterPoints;
            using (var input = new StreamReader(path + postfix, Encoding.Default))
            {
                var temp = input.ReadLine().Split(new char[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                name = temp[3];
                temp = input.ReadToEnd().Split(new char[] {'\n'}, StringSplitOptions.RemoveEmptyEntries);
                string[] mass;
                for (var i = 0; i < temp.Length-2; i++)
                {
                    mass = temp[i].Split(new char[] {'\t', '\n'}, StringSplitOptions.RemoveEmptyEntries);
                    rentalsData[mass[0]] = double.Parse(mass[1]);
                }
                mass = temp[temp.Length - 2].Split(new char[] {' ', '\n'}, StringSplitOptions.RemoveEmptyEntries);
                totalAmount = double.Parse(mass[3]);
                mass = temp[temp.Length - 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                frequentRenterPoints = int.Parse(mass[2]);
            }
            return new DataStore(name,rentalsData,totalAmount,frequentRenterPoints);
        }
    }
}
