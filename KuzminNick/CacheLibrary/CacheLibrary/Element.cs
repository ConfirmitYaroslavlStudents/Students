namespace CacheLibrary
{
    public class Element<T>
    {
        private T _value;
        private readonly string _identifier;
        private int _frequenceUsage;
        private int _timeLastUsingInSeconds;

        public Element(string identifier, T value)
        {
            _identifier = identifier;
            Value = value;
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
        public int TimeLastUsingInSeconds
        {
            get { return _timeLastUsingInSeconds; }
            set { _timeLastUsingInSeconds = value; }
        }

        public override string ToString()
        {
            return string.Format("{0}", Value);
        }
    }
}
