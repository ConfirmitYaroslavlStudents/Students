using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph<string> gr = new Graph<string>("A");

            gr.AddVertex("B", new HashSet<string>());
            gr.AddVertex("C", new HashSet<string>(new string[] { "A" }));
            gr.AddVertex("D", new HashSet<string>(new string[] { "C", "B" }));
            
            foreach (var vertex in gr)
            {
                Console.WriteLine();
                Console.Write(vertex.Key + " With ");
                foreach (var edge in vertex.Value)
                    Console.Write(edge + " ");
            }
            Console.ReadKey();
        }
    }
}
