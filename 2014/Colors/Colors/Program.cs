using System;
using System.Collections.Generic;
using Colors.Colors;
using Colors.Visitors;

namespace Colors
{
    class Program
    {
        static void Main(string[] args)
        {
            var firsts = new List<IColor>
            {
                new Red(),
                new Green(),
                new Blue()
            };

            var seconds = new List<IColor>
            {
                new Red(),
                new Green(),
                new Blue()
            };

            var firstVisitor = new FirstVisitor(new ColorProcessor());

            foreach (var first in firsts)
            {
                var secondVisitor = first.AcceptVisitor(firstVisitor);

                foreach (var second in seconds)
                {   
                    Console.WriteLine(second.AcceptVisitor(secondVisitor));
                }
            }
            
        }
    }
}
