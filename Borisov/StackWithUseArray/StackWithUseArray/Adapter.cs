using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace StackWithUseArray
{
    class Adapter<T> : Stack<T>

    {
        public override IEnumerator<T> GetEnumerator()
        {
            return new Adaptee<T>.ArrayEnumerator(base._array,base.Length);
        }
    }
}
