using System.Collections.Generic;
using System.IO;

namespace CacheLibrary.UnitTests
{
    public class DataBaseGenerator
    {
        private readonly Dictionary<string, Element<string>> _stringDictionary = new Dictionary<string, Element<string>>();

        public DataBaseGenerator (int amountOfElements)
        {
            var counter = 0;
            using (var streamReader = new StreamReader("ListOfElements.txt"))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    var id = line.GetHashCode().ToString();
                    var newElement =
                        new Element<string>(id, line);
                    _stringDictionary.Add(id, newElement);
                    counter++;
                    if (counter == amountOfElements)
                        break;
                }
            }
        }

        public Dictionary<string, Element<string>> StringDictionary
        {
            get { return _stringDictionary; }
        }
    }
}