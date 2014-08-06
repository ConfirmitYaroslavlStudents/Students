using System.Collections.Generic;
using System.IO;

namespace CacheLibrary
{
    public class GeneratorRandomStrings
    {
        private readonly Dictionary<string, Element<string>> _stringDictionary = new Dictionary<string, Element<string>>();

        public Dictionary<string, Element<string>> StringDictionary
        {
            get { return _stringDictionary; }
        }

        public int GetCount()
        {
            return _stringDictionary.Count;
        }

        public GeneratorRandomStrings(int amount)
        {
            GenerateUniqueRandomElements(amount);
        }

        private void GenerateUniqueRandomElements(int amount)
        {
            for (var i = 0; i < amount; i++)
            {
                var newElement = GenerateRandomElement();
                _stringDictionary.Add(newElement.Id, newElement);
            }
        }

        private Element<string> GenerateRandomElement()
        {
            var randomString = Path.GetRandomFileName();
            var id = randomString.GetHashCode().ToString();
            var newElement =
                new Element<string>(id, randomString);
            return newElement;
        }
    }
}