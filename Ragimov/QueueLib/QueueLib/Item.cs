using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueLib
{
    internal class Item<T>
    {
        private readonly T _value;

        public Item(T value)
        {
            _value = value;
        }

        public T Value
        {
            get { return _value; }
        }

        public Item<T> Next { get; set; }
    }
}
