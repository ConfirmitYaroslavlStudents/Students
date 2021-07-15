using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina
{
    public class Application
    {
        private ToDoList list;

        public Application()
        {
            list = new ToDoList();
        }

        public void Read()
        {
            list.Read();
        }

        public void Write()
        {
            list.Write();
        }

        public void Print()
        {
            if (list.Count() == 0)
            {
                Console.WriteLine("Лист пуст");
                return;
            }

            list.Print();
        }

        public void Add()
        {
            Console.Write("Введите название миссии: ");
            var name = Console.ReadLine();
            if (name.Length == 0)
            {
                Console.WriteLine("Введена некорректная строка ");
                return;
            }

            list.Add(new Task(name, 0));
        }

        public void Delete()
        {
            Console.Write("num = ");
            var inputStr = Console.ReadLine();
            if (Int32.TryParse(inputStr, out int num) == false)
            {
                Console.WriteLine("Введены некорректные данные ");
                return;
            }
            if (num < 1 || num > list.Count())
            {
                Console.WriteLine("Введена некорректная строка ");
                return;
            }

            list.Detete(num);
        }

        public void Edit()
        {
            Console.Write("num = ");
            var inputStr = Console.ReadLine();
            if (Int32.TryParse(inputStr, out int num) == false)
            {
                Console.WriteLine("Введены некорректные данные ");
                return;
            }
            if (num < 1 || num > list.Count())
            {
                Console.WriteLine("Введена некорректная строка ");
                return;
            }

            list.Edit(num);
        }

        public void ChangeStatus()
        {
            Console.Write("num = ");
            var inputStr = Console.ReadLine();
            if (Int32.TryParse(inputStr, out int num) == false)
            {
                Console.WriteLine("Введены некорректные данные ");
                return;
            }
            if (num < 1 || num > list.Count())
            {
                Console.WriteLine("Введена некорректная строка ");
                return;
            }

            list.ChangeStatus(num);
        }

    }
}
