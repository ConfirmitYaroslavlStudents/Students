using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Guitar_shop
{
    class Shop 
    {
        private List<Instrument> _instruments;
        private string FileName;

        public Shop()
        {
            _instruments = new List<Instrument>();
            FileName = null;
        }

        public void AddFromFile(string fileName)
        {
            var lines = File.ReadAllLines(fileName);

            for (int i = 1; i < lines.Length; i++)
            {
                Guitar guitar = new Guitar();
                _instruments.Add(guitar.Make(lines[i]));
            }

            if (FileName == null) FileName = fileName;
        }

        public void Add(Instrument instrument)
        {
            _instruments.Add(instrument);
        }

        public void Delete(string ID)
        {
            foreach (Instrument instrument in _instruments)
            {
                if (instrument.ID == ID)
                {
                    _instruments.Remove(instrument);
                }
            }
        }

        public List<Instrument> Search(string searchCondition)
        {
            var suitableInstruments = new List<Instrument>();

            foreach (Instrument instrument in _instruments)
            {
                if (instrument.ContainsTerm(searchCondition))
                {
                    suitableInstruments.Add(instrument);
                }
            }
            return suitableInstruments;
        }
        public void AddFromConsole()
        {
            Guitar guitar = new Guitar();
            guitar.ConsoleWriteTerms();
            var line = Console.ReadLine();
            _instruments.Add(guitar.Make(line));
            File.AppendAllText(FileName, Environment.NewLine + line);
        }

    }
}
