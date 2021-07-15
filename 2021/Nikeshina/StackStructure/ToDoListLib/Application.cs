using System;
using System.Collections.Generic;

namespace ToDoListLib
{
    public class ToDoList
    {
        private List<Task> list;
        private WorkWithFile fileEditor;
        public ToDoList()
        {
            list = new List<Task>();
            fileEditor = new WorkWithFile();
        }
        public int Count()
        {
            return list.Count;
        }
        public void Read() 
        {
            list = fileEditor.Read();
        }
        public void Write()
        {
            fileEditor.Write(list);
        }
        public void Print()
        {
            int i = 1;
            foreach(var task in list)
            {
                Console.WriteLine(i+". "+task.Print());
                i++;
            }
        }
        public void Add(Task item) 
        {
            list.Add(item);
            Print();
        }
        public void Detete(int num)
        {
            list.RemoveAt(num - 1);
            Print();
        }
        public void ChangeStatus(int num)
        {
            list[num - 1].Status = (list[num - 1].Status + 1) % 2;
        }


    }
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
            if(list.Count()==0)
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

            list.Add(new Task(name,0));
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
