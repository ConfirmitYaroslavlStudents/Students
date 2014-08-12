using System;

namespace CacheLibrary
{
    public enum TypesOfStorage { DataBase, Cache };

    public class Element<T>
    {
        private readonly string _id;
        private long _timeOfLastUsingInTicks;

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

        public int FrequencyUsage { get; private set; }

        public void IncrementFrequencyUsage()
        {
            FrequencyUsage++;
        }

        public long TimeOfLastUsingInTicks
        {
            get { return _timeOfLastUsingInTicks; }
            set 
            {
                if (value >= 0)
                    _timeOfLastUsingInTicks = value;
                else
                    throw new ArgumentOutOfRangeException();
            }
        }

        public TypesOfStorage TypeOfStorage { get; set; }

        public override string ToString()
        {
            return string.Format("Value of Element = '{0}', Id = '{1}', Type of Storage = '{2}', " +
                                 "Time Of Last Using In Ticks = '{3}', frequency = '{4}'", 
                                 Value, Id, TypeOfStorage, TimeOfLastUsingInTicks, FrequencyUsage);
        }
    }
}
