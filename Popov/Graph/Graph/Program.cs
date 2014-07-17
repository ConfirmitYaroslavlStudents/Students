using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    class Program
    {
        static void Main(string[] args)
        {
            var gr = new Graph<string>("A");
            gr.AddVertex("B", new HashSet<string>(new[] { "A" }));
            gr.AddVertex("C", new HashSet<string>(new[] { "A" }));
            gr.AddVertex("D", new HashSet<string>(new[] { "C" }));
            gr.AddVertex("H", new HashSet<string>(new[] { "D" }));
            gr.AddVertex("R", new HashSet<string>(new[] { "C" }));
            gr.AddVertex("M", new HashSet<string>(new[] { "D" }));
            gr.AddVertex("K", new HashSet<string>(new[] { "B" }));

            
            var menu = new ConsoleMenu();

            menu.AddCommand("Print graph to console", () => PrintToConsole(gr));
            menu.AddCommand("Print graph to console as matrix", () => PrintToConsoleMatrix(gr));
            menu.AddCommand("View process going graph in width", () => gr.ViewWidth("C"));
            menu.AddCommand("View process going graph in depth", () => gr.ViewDepth("C"));
            menu.Show();


            try
            {
                while (true)
                {
                    Console.WriteLine();
                    Console.WriteLine("Enter the number point of menu or any symbol for exit");
                    var s = Console.ReadLine();
                    if (s != "e")
                    {
                        var result = int.Parse(s);
                        menu.LaunchCommand(result);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (Exception) { }

            
            

        }

        static void PrintToConsole<T>(IEnumerable<KeyValuePair<T, HashSet<T>>> gr)
        {
            foreach (var vertex in gr)
            {
                Console.WriteLine();
                Console.Write(vertex.Key.ToString() + " With ");
                foreach (var edge in vertex.Value)
                    Console.Write(edge.ToString() + " ");
            }
        }

        static void PrintToConsoleMatrix<T>(Graph<T> gr)
        {
            var matric = gr.ToAdjacencyMatrixy();

            for (var i = 0; i < matric.GetLength(0); ++i)
            {
                Console.WriteLine();
                for (var j = 0; j < matric.GetLength(1); ++j)
                {
                    Console.Write(matric[i, j] ? "1 " : "0 ");
                }
            }
        }
    }
}
