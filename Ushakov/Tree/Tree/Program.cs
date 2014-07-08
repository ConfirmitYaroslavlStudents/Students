using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    class Program
    {
        static void Main(string[] args)
        {
            Tree<int> intTree = new Tree<int>();
            Random rnd = new Random();
            for (int i = 0; i < 15; i++)
            {
                intTree.Add(rnd.Next(20));
            }


            PrintTreeTraversing(intTree, "Прямой обход (по умолчанию): ");

            intTree.Traversing = new ReverseTraversing<int>();
            PrintTreeTraversing(intTree, "Обратный обход: ");
            
            intTree.Traversing = new SymmetricTraversing<int>();
            PrintTreeTraversing(intTree, "Симметричный обход: ");
            
            Console.ReadLine();
        }

        private static void PrintTreeTraversing(Tree<int> intTree, string TraversingName)
        {
            List<int> valueList = intTree.Traverse();
            Console.Write(TraversingName);
            PrintList<int>(valueList);
        }

        static void PrintList<T>(List<T> valueList)
        {
            for (int i = 0; i < valueList.Count; i++)
                Console.Write(valueList[i].ToString() + " ");
            Console.WriteLine();
        }
    }
}
