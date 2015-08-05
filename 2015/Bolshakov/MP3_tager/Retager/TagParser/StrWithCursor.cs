using System;

namespace Mp3Handler
{
    public class StrWithCursor
    {
        protected bool Equals(StrWithCursor other)
        {
            return string.Equals(_str, other._str) && _cursor == other._cursor;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((StrWithCursor)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((_str != null ? _str.GetHashCode() : 0) * 397) ^ _cursor;
            }
        }

        public override string ToString()
        {
            return _str[Cursor].ToString();
        }

        public StrWithCursor(string input)
        {
            StringValue = input;
        }

        public StrWithCursor(StrWithCursor input)
        {
            _str = input._str;
            _cursor = input._cursor;
        }

        public int Length { get { return _str.Length; } }

        public int Cursor
        {
            get { return _cursor; }
            private set
            {
                if (value < Length) _cursor = value;
                else throw new ArgumentOutOfRangeException();
            }
        }

        public char Value { get { return StringValue[Cursor]; } }

        public string StringValue
        {
            get { return _str; }
            set
            {
                if (value.Length != 0)
                    _str = value;
                else
                    throw new ArgumentOutOfRangeException("String can't be empty");
                Cursor = 0;
            }
        }

        public static bool operator ==(StrWithCursor first, StrWithCursor second)
        {
            return first.Value == second.Value;
        }

        public static bool operator !=(StrWithCursor first, StrWithCursor second)
        {
            return !(first == second);
        }

        public static bool operator ==(StrWithCursor first, char second)
        {
            return first.Value == second;
        }

        public static bool operator !=(StrWithCursor first, char second)
        {
            return !(first == second);
        }

        public static StrWithCursor operator ++(StrWithCursor input)
        {
            if (input._cursor < input.Length - 1)
                input._cursor++;
            return input;
        }

        public static StrWithCursor operator --(StrWithCursor input)
        {
            if (input._cursor > 1)
                input._cursor--;
            return input;
        }

        private string _str;
        private int _cursor;
    }
}
