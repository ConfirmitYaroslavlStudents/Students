using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace DirectGraph
{
    public class DirectedGraphReader
    {
        [DllImport("kernel32.dll")]
        public static extern Boolean AllocConsole();
        [DllImport("kernel32.dll")]
        public static extern Boolean FreeConsole();

        static public DirectedGraph ReadFromTxtFile(string fileName)
        {
            StreamReader inputFile = new StreamReader(fileName);
            string fileContent = inputFile.ReadToEnd();

            string[] inputData = fileContent.Split(new string[] { " ", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            var result = new DirectedGraph(int.Parse(inputData[0]));

            int edgesCount = int.Parse(inputData[1]);
            for (int i = 0; i < edgesCount; ++i)
            {
                DirectedEdge edge = new DirectedEdge(int.Parse(inputData[i * 2 + 2]) - 1, int.Parse(inputData[i * 2 + 3]) - 1);
                result[edge.Begin].Add(edge);
            }

            return result;
        }

        static public DirectedGraph ReadFromConsole()
        {
            AllocConsole();

            Console.Write("Enter a number of vertices, please: ");
            int verticesCount = int.Parse(Console.ReadLine());
            DirectedGraph result = new DirectedGraph(verticesCount);

            Console.Write("Enter a number of edges, please: ");
            int edgesCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < edgesCount; ++i)
            {
                Console.Write("Enter begin and end of {0} edge, please: ", i + 1);
                string[] inputData = Console.ReadLine().Split(new string[]{" "}, StringSplitOptions.RemoveEmptyEntries);

                DirectedEdge edge = new DirectedEdge(int.Parse(inputData[0]) - 1, int.Parse(inputData[1]) - 1);
                result[edge.Begin].Add(edge);
            }

            FreeConsole();
            return result;
        }
    }
}
