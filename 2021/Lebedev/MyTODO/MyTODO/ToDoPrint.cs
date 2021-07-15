using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace MyTODO
{
    class ToDoPrint
    {
        static ToDoList todo;
        static bool deleted = false;
        static bool completed = true;
        static ToDoItem item;
        static int count=0;
        static int menuindexX = 0;
        static int menuindexY = 0;
        static FileInfo input = new FileInfo("TODOsave.txt");

        static void SetDefaultColor()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void SetChosenColor()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
        }

        static void PrintChosen(string str)
        {
            SetChosenColor();

            Console.Write(str);

            SetDefaultColor();
        }

        static void PrintOk(string str, bool ok)
        {
            if (ok)
                Console.ForegroundColor = ConsoleColor.Green;
            else
                Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(str);
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void SetNameColorAndPrint(string str, int state)
        {
            if(state == (int)States.deleted || state == (int)States.completeddeleted)
                PrintOk(str, false);
            else
                Console.Write(str);
        }

        static void PrintName(ToDoItem item, bool chosen)
        {
            if (chosen) 
                SetChosenColor();
            if (item.Name.Length <= 20)
            {
                SetNameColorAndPrint(item.Name, item.State);
                for (int i = item.Name.Length; i < 19; i++)
                    Console.Write(" ");
            }
            else
            {
                int x = 0;
                while (x < item.Name.Length)
                {
                    if (x != 0)
                    {
                        if (chosen)
                            SetChosenColor();
                        Console.WriteLine("║ ║");
                        Console.Write("║");
                        if (chosen)
                            SetDefaultColor();
                    }
                    for (int i = 0; i < 18; i++, x++)
                        if (x < item.Name.Length)
                            SetNameColorAndPrint(item.Name[x].ToString(), item.State);
                        else
                            Console.Write(" ");
                    if (x < item.Name.Length)
                        if (item.Name[x] != ' ')
                            SetNameColorAndPrint("-", item.State);
                        else
                            SetNameColorAndPrint(item.Name[x++].ToString(), item.State);
                    else
                        Console.Write(" ");
                }
            }
            if (chosen)
                SetDefaultColor();
        }

        static void PrintMenu()
        {
            string[] menu = new string[] { "Add", "Deleted", "Completed" };
            Console.Clear();
            Console.WriteLine("╔═══╦═══════╦═════════╗");
            Console.Write("║");
            var items = todo.FindAll(x => x.State == 0 || 
                                         (x.State == (int)States.completed && completed) || 
                                         (x.State == (int)States.deleted && deleted) || 
                                         (x.State == (int)States.completeddeleted && deleted && completed));
            count = items.Count;
            menuindexX %= count + 1;
            for (int i = 0; i < 3; i++)
            {
                if (menuindexX == 0 && menuindexY == i)
                    PrintChosen(menu[i]);
                else
                    switch (i)
                    {
                        case 0:
                            Console.Write(menu[i]);
                            break;
                        case 1:
                            PrintOk(menu[i], deleted);
                            break;
                        case 2:
                            PrintOk(menu[i], completed);
                            break;
                    }
                Console.Write("║");
            }
            Console.WriteLine();
            if (count == 0)
            {
                Console.WriteLine("╚═══╩═══════╩═════════╝");
            }
            else
                Console.WriteLine("╠═══╩═══════╩═══════╦═╣");
            int j = 0;
            foreach (var each in items)
            {
                if (menuindexX == j + 1)
                    item = each;
                Console.Write("║");
                PrintName(each, menuindexX == j + 1 && menuindexY == 0);
                Console.Write("║");
                string state = "";
                switch (each.State)
                {
                    case (int)States.deleted:
                        state = "X";
                        break;
                    case (int)States.uncompleted:
                        state = " ";
                        break;
                    case (int)States.completed:
                    case (int)States.completeddeleted:
                        state = "√";
                        break;
                }
                if (menuindexX == j + 1 && menuindexY == 1)
                    PrintChosen(state);
                else
                    PrintOk(state,each.State== (int)States.completed || each.State == (int)States.completeddeleted);
                Console.WriteLine("║");
                if (j + 1 == count)
                    Console.WriteLine("╚═══════════════════╩═╝\nDel to delete item");
                else
                    Console.WriteLine("╠═══════════════════╬═╣");
                j++;
            }
        }

        static void WorkWithMenu()
        {
            menuindexX %= count + 1;
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.UpArrow:
                    menuindexX = (count + menuindexX) % (count + 1);
                    if (menuindexX != 0)
                        menuindexY %= 2;
                    break;
                case ConsoleKey.DownArrow:
                    menuindexX = (menuindexX + 1) % (count + 1);
                    if (menuindexX != 0)
                        menuindexY %= 2;
                    break;
                case ConsoleKey.LeftArrow:
                    if (menuindexX == 0)
                        menuindexY = (menuindexY + 2) % 3;
                    else
                        menuindexY = (menuindexY + 1) % 2;
                    break;
                case ConsoleKey.RightArrow:
                    if (menuindexX == 0)
                        menuindexY = (menuindexY + 1) % 3;
                    else
                        menuindexY = (menuindexY + 1) % 2;
                    break;
                case ConsoleKey.Enter:
                    EnterClick();
                    break;
                case ConsoleKey.Delete:
                    if (menuindexX == 0)
                        break;
                    item.Delete();
                    ToDoListReducer.Save(input, todo);
                    if (!deleted)
                        count--;
                    break;
            }
        }
        public static void EnterClick()
        {
            if (menuindexX == 0)
            {
                switch (menuindexY)
                {
                    case 0:
                        string name = "";
                        while (string.IsNullOrEmpty(name))
                        {
                            Console.Clear();
                            Console.WriteLine("Write new TODO:");
                            name = Console.ReadLine();
                        }
                        todo.Add(name);
                        ToDoListReducer.Save(input, todo);
                        break;
                    case 1:
                        deleted = !deleted;
                        break;
                    case 2:
                        completed = !completed;
                        break;
                }
                return;
            }
            Console.Clear();
            if (menuindexX == 0)
                return;
            if (menuindexY == 0)
            {
                Console.WriteLine("Write new name for {0}", todo[menuindexX - 1].Name);
                string x = Console.ReadLine();
                while (string.IsNullOrEmpty(x))
                {
                    Console.Clear();
                    Console.WriteLine("Write new name for {0}", todo[menuindexX - 1].Name);
                    x = Console.ReadLine();
                }
                item.ChangeName(x);
                ToDoListReducer.Save(input, todo);
            }
            else
            {
                item.Complete();
                ToDoListReducer.Save(input, todo);
                if (!completed)
                    count--;
            }
        }
        static void Main(string[] args)
        {
            todo = new ToDoList(input);
            while (true)
            {
                PrintMenu();
                WorkWithMenu();
            }
        }
    }
}
