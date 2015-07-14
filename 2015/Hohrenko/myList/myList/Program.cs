using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyList
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> testList = new List<string>();

            testList.Add("one");
            testList.Add("two");
            testList.Add("three");
            testList.Add("four");
            testList.Add("five");
            testList.Add("six");
            testList.Add("seven");
            testList.Add("eight");


            //testList.Remove("six");

            testList.Insert(7, "insert1");
            testList.Insert(9, "insert2");


            foreach (var s in testList)
            {
                Console.WriteLine(s);
            }

        }
    }
}
