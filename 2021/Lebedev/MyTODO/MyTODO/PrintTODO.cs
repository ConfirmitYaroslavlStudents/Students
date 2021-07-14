using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace MyTODO
{
    class PrintTODO
    {
        static ListTODO todo;
        static bool deleted = false;
        static bool completed = true;
        static ListTODO.ItemTODO item;
        static int count=0;
        static int menuindexX = 0;
        static int menuindexY = 0;
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

        static void PrintOk(string str, bool Ok)
        {
            if (Ok)
                Console.ForegroundColor = ConsoleColor.Green;
            else
                Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(str);
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void PrintName(string str, bool chosen)
        {
            if (chosen) 
                SetChosenColor();
            if (str.Length <= 20)
            {
                Console.Write(str);
                for (int i = str.Length; i < 19; i++)
                    Console.Write(" ");
            }
            else
            {
                int x = 0;
                while (x < str.Length)
                {
                    if (x != 0)
                    {
                        if (chosen)
                        {
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        Console.WriteLine("║ ║");
                        Console.Write("║");
                        if (chosen)
                        {
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                    }
                    for (int i = 0; i < 18; i++, x++)
                        if (x < str.Length)
                            Console.Write(str[x]);
                        else
                            Console.Write(" ");
                    if (x < str.Length)
                        if (str[x] != ' ')
                            Console.Write("-");
                        else
                            Console.Write(str[x++]);
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
            var items = todo.items.FindAll(x => x.state == 0 || (x.state == 1 && completed) || (x.state == -1 && deleted));
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
                PrintName(each.name, menuindexX == j + 1 && menuindexY == 0);
                Console.Write("║");
                string state = "";
                switch (each.state)
                {
                    case -1:
                        state = "X";
                        break;
                    case 0:
                        state = " ";
                        break;
                    case 1:
                        state = "√";
                        break;
                }
                if (menuindexX == j + 1 && menuindexY == 1)
                    PrintChosen(state);
                else
                    PrintOk(state,each.state==1);
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
                    item.ChangeState(-1);
                    todo.Save();
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
                        todo.Save();
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
                Console.WriteLine("Write new name for {0}", todo.items[menuindexX - 1].name);
                string x = Console.ReadLine();
                while (string.IsNullOrEmpty(x))
                {
                    Console.Clear();
                    Console.WriteLine("Write new name for {0}", todo.items[menuindexX - 1].name);
                    x = Console.ReadLine();
                }
                item.ChangeName(x);
                todo.Save();
            }
            else
            {
                item.ChangeState(1);
                todo.Save();
                if (!completed)
                    count--;
            }
        }
        static void Main(string[] args)
        {
            FileInfo input = new FileInfo("TODOsave.txt");
            todo = new ListTODO(input);
            while (true)
            {
                PrintMenu();
                WorkWithMenu();
            }
        }
    }
}
