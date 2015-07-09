using System;

namespace CacheLibrary
{
    public enum TypesOfStorage { DataBase, Cache };

    public class Element<T>
    {
        private readonly string _id;
        private int _timeOfLastUsingInSeconds;

        public Element(string id, T value)
        {
            _id = id;
            FrequencyUsage = 0;
            Value = value;
            TypeOfStorage = TypesOfStorage.DataBase;
        }

        public string Id
        {
            get { return _id; }
        }

        public T Value { get; set; }

        public int FrequencyUsage { get; private set; } //I suppose UsageFrequency is better name for this

        public void IncrementFrequencyUsage() //Again, UsageFrequency
        {
            FrequencyUsage++;
        }

        public int TimeOfLastUsingInSeconds
        {
            get { return _timeOfLastUsingInSeconds; }
            set 
            {
                if (value >= 0)
                    _timeOfLastUsingInSeconds = value;
                else
                    throw new ArgumentOutOfRangeException();
            }
        }

        public TypesOfStorage TypeOfStorage { get; set; }

        public override string ToString()
        {
            return string.Format("Value of Element = '{0}', Id = '{1}', Type of Storage = '{2}'", Value, Id, TypeOfStorage);
        }
    }
}
