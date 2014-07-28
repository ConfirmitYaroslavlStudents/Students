namespace CacheWithoutTimers
{
    public class Element<T>
    {
        private T _value;
        private readonly string _identifier;
        private int _frequenceUsage;
        private int _timeOfLastUsingInSeconds;

        public Element(string identifier, T value)
        {
            _identifier = identifier;
            FrequencyUsage = 0;
            Value = value;
            TypeOfStorage = "Data Base";
        }

        public T Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public string Identifier
        {
            get { return _identifier; }
        }

        public int FrequencyUsage
        {
            get { return _frequenceUsage; }
            set { _frequenceUsage = value; }
        }

        public int TimeOfLastUsingInSeconds
        {
            get { return _timeOfLastUsingInSeconds; }
            set { _timeOfLastUsingInSeconds = value; }
        }

        public string TypeOfStorage { get; set; }

        public override string ToString()
        {
            return string.Format("Value of Element = '{0}', Id = '{1}', Type of Storage = '{2}'", Value, Identifier, TypeOfStorage);
        }
    }
}
