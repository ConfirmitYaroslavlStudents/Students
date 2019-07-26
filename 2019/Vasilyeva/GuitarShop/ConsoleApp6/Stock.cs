using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp6
{
    public class Stock
    {
        private List<Instrument> item = new List<Instrument>();
        
        public ReadData()
        {
            var lines = File.ReadAllLines("guitars.txt");
            var guitars = new List<Guitar>();

            foreach (var line in lines)
            {
                item.Add(MakeGuitar(line));
            }
        }
    }
}
