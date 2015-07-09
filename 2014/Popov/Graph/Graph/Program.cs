using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Graph
{
    internal class Program
    {
        private static void Main()
        {
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            

            var gr = new Graph<string>("A");
            gr.AddVertex("T");
            gr.AddVertex("B", new HashSet<string>(new[] {"A"}));
            gr.AddVertex("C", new HashSet<string>(new[] {"A"}));
            gr.AddVertex("D", new HashSet<string>(new[] {"C"}));
            gr.AddVertex("H", new HashSet<string>(new[] {"D"}));
            gr.AddVertex("R", new HashSet<string>(new[] {"C"}));
            gr.AddVertex("M", new HashSet<string>(new[] {"D"}));
            gr.AddVertex("K");


            var menu = new ConsoleMenu();

            menu.AddCommand("Print graph to console", () => Print(gr));
            menu.AddCommand("Print graph to console as matrix", () => PrintTMatrix(gr));
            menu.AddCommand("View process going graph in width",
                () => gr.ViewWidth("T", s => Trace.Write(s + " ")));
            menu.AddCommand("View process going graph in depth",
                () => gr.ViewDepth("T", s => Trace.Write(s + " ")));
            menu.Show();

            
                while (true)
                {
                    Trace.WriteLine(Environment.NewLine);
                    Trace.WriteLine("Enter the number point of menu or any symbol for exit");
                    var symbol = Console.ReadLine();
                    if (!IsNumber(symbol)) break;
                    if (menu.ContainsNumberCommande(int.Parse(symbol)))
                    {
                        var result = int.Parse(symbol);
                        menu.LaunchCommand(result);
                    }
                    else
                    {
                        break;
                    }
                }
            


        }

        private static void Print<T>(IEnumerable<KeyValuePair<T, HashSet<T>>> gr)
        {
            foreach (var vertex in gr)
            {
                Trace.WriteLine(Environment.NewLine);
                Trace.Write(vertex.Key.ToString() + " With ");
                foreach (var edge in vertex.Value)
                    Trace.Write(edge.ToString() + " ");
            }
        }

        private static void PrintTMatrix<T>(Graph<T> gr)
        {
            var matric = gr.ToAdjacencyMatrix();

            for (var i = 0; i < matric.GetLength(0); ++i)
            {
                Trace.WriteLine(Environment.NewLine);
                for (var j = 0; j < matric.GetLength(1); ++j)
                {
                    Trace.Write(matric[i, j] ? "1 " : "0 ");
                }
            }
        }

        private static bool IsNumber(string number)
        {
            int convertDigit;
            return int.TryParse(number, out convertDigit);
        }

        
    }

}
