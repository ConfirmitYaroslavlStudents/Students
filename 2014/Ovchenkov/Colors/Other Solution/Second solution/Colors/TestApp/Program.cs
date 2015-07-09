using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colors;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var red = new Red();
           var green = new Green();
            Console.WriteLine(red.ToString());
            Console.WriteLine(green.ToString());
          //  Console.WriteLine(red.GetMyType());
           // ColorsProcessor.Process(red, green);
        }
    }
}
