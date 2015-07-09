using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Refactoring;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var customer = new Customer("nobody");
            var statement = new Statement(customer);
            Console.WriteLine(statement.GetStatement(new StringStatement()));
        }
    }
}
