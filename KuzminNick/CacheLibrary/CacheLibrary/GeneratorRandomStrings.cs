using System;
using System.Collections.Generic;
using System.IO;
using CacheWithoutTimers;

namespace CacheLibraryWithoutTimers
{
    public class GeneratorRandomStrings
    {
        private readonly Dictionary<string, Element<string>> _stringDictionary = new Dictionary<string, Element<string>>();

        public Dictionary<string, Element<string>> StringDictionary
        {
            get { return _stringDictionary; }
        }

        private readonly Random _randomizerId = new Random();

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
                var newElement = GenerateRandomElement(_randomizerId);
                _stringDictionary.Add(newElement.Identifier, newElement);
            }
        }

        private Element<string> GenerateRandomElement(Random randomizerId)
        {
            var randomString = Path.GetRandomFileName();
            var identifier = randomString.GetHashCode().ToString();
            var newElement =
                new Element<string>(identifier, randomString);
            //TODO !!!
            //TODO !!!
            Console.WriteLine(newElement);
            return newElement;
        }
    }
}