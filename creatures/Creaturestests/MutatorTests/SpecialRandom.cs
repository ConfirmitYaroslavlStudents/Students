using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Creaturestests.MutatorTests
{
    internal class SpecialRandom : Random
    {
        private int[] _return;
        private int index;
        private static Random Rnd = new Random();

        public SpecialRandom(params int[] ret)
        {
            _return = ret;
            index = 0;
        }
        public override int Next()
        {
            if (index != _return.Length)
                return _return[index++];
            else
                return Rnd.Next();
        }

        public override int Next(int first)
        {
            if (index != _return.Length)
                return _return[index++];
            else
                return Rnd.Next(first);
        }

        public override int Next(int first, int second)
        {
            if (index != _return.Length)
                return _return[index++];
            else
                return Rnd.Next(first, second);
        }
    }
}
