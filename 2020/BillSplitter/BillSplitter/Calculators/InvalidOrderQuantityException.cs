using System;

namespace BillSplitter.Calculators
{
    public class InvalidOrderQuantityException : Exception
    {
        public InvalidOrderQuantityException(string message)
            : base(message)
        {
        }
    }
}