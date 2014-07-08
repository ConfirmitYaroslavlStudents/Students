using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace FizzBuzz
{
    public class FizzBuzzer
    {
        private Dictionary<int, string> _settings;

        public Dictionary<int, string> Settings
        {
            get { return _settings; }
            set { }
        }
        public FizzBuzzer()
        {
            _settings = new Dictionary<int, string>();
            _settings.Add(3,"Fizz");
            _settings.Add(5,"Buzz");
        }
        public string ToString(int number)
        {
            var result = new List<string>();
            string newWord = string.Empty;
            foreach (int key in _settings.Keys)
            {
                if (number % key == 0)
                {
                    _settings.TryGetValue(key, out newWord);
                    result.Add(newWord);
                }
            }

            if (result.Count > 0)
                return string.Join(" ", result);

            return number.ToString();
        }
        public void AddSetting(int number, string word)
        {
            if (_settings.ContainsKey(number))
                _settings.Remove(number);
            _settings.Add(number, word);
        }
    }
}
