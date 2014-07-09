using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListRealization
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> myIntList = new List<int>();
            //-----------------
            myIntList.Add(1);
            myIntList.Add(2);
            myIntList.Add(3);
            myIntList.Add(4);
            myIntList.Add(5);
            myIntList.Add(6);
            myIntList.Add(7);
            //-----------------
            myIntList.PRINT();
            //-----------------
            myIntList.RemoveAt(1);
            myIntList.Remove(6);
            //-----------------
            myIntList.PRINT();
            Console.ReadKey();
        }
    }
}
