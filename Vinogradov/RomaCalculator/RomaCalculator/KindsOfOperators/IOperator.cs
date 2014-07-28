using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomaCalculator.KindsOfOperators
{
    public interface IOperator
    {
        int CalculateIt(int a, int b);
    }
}
