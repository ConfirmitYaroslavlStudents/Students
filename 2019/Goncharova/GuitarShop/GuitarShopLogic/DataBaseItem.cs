using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarShopLogic
{
    internal abstract class DataBaseItem
    {
        public DataBaseItem(string line) { }

        internal abstract bool Contains(string term);
    }
}
