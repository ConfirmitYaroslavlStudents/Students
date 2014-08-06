using System.Collections.Generic;
using System.IO;

namespace FilmService.KindsOfGenerators
{
    public class StatementGeneratorString:StatementGenerator
    {
        public override void Generate(string path)
        {
            var result = "Учет аренды для " + CurrentData.Name + "\n";
            foreach (var item in CurrentData.RentalsData)
            {
                result += "\t" + item.Key + "\t" + item.Value + "\n";
            }
            result += "Сумма задолженности составляет " + CurrentData.TotalAmount + "\n";
            result += "Вы заработали " + CurrentData.FrequentRenterPoints + " за активность";
            using (var output = new StreamWriter(path, true))
            {
                output.Write(result);
            }
        }
    }
}
