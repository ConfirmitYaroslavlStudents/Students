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
            for (int i = 0; i < 10; i++)
                intTree.Add(rnd.Next(20));

            List<int> valueList = intTree.DirectTraversing();
            Console.Write("Прямой обход: ");
            PrintList<int>(valueList);

            valueList = intTree.ReverseTraversing();
            Console.Write("Обратный обход: ");
            PrintList<int>(valueList);

            valueList = intTree.SymmetricTraversing();
            Console.Write("Симметричный обход: ");
            PrintList<int>(valueList);

            Console.WriteLine("Удаление элементнов дерева (вывод результата при симметричном обходе)");
            while (valueList.Count > 0)
            {
                int removeIndex = rnd.Next(valueList.Count);
                Console.WriteLine(valueList[removeIndex]);
                intTree.Remove(valueList[removeIndex]);

                valueList = intTree.SymmetricTraversing();
                PrintList<int>(valueList);
            }

            Console.ReadLine();
        }

        static void PrintList<T>(List<T> valueList)
        {
            for (int i = 0; i < valueList.Count; i++)
                Console.Write(valueList[i].ToString() + " ");
            Console.WriteLine();
        }
    }
}
