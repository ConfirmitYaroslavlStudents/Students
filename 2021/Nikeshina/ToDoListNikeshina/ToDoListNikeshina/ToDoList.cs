using System;
using System.Collections.Generic;

namespace ToDoListNikeshina
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
            foreach (var task in list)
            {
                Console.WriteLine(i + ". " + task.Print());
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
        }

        public void ChangeStatus(int num)
        {
            list[num - 1].Status = (list[num - 1].Status + 1) % 2;
        }

        public void Edit(int index)
        {
            Console.Write("Введите новое название задачи: ");
            var str = Console.ReadLine();
            if (str.Length == 0)
            {
                Console.WriteLine("Введена некорректная строка ");
                return;
            }

            list[index - 1].Name = str;
            Print();
        }

    }
}
