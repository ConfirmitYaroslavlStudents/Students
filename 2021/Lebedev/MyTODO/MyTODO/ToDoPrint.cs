using System;
using System.IO;

namespace MyTODO
{
    internal class ToDoPrint
    {
        private static ToDoList _todo;
        private static bool _deleted;
        private static bool _completed = true;
        private static ToDoItem _item;
        private static int _count;
        private static int _menuIndexX;
        private static int _menuIndexY;
        private static readonly FileInfo Input = new FileInfo("TODOsave.txt");

        private static void SetDefaultColor()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void SetChosenColor()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
        }

        private static void PrintChosen(string str)
        {
            SetChosenColor();

            Console.Write(str);

            SetDefaultColor();
        }

        private static void PrintOk(string str, bool ok)
        {
            if(ok)
                Console.ForegroundColor = ConsoleColor.Green;
            else
                Console.ForegroundColor = ConsoleColor.Red;

            Console.Write(str);

            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void SetNameColorAndPrint(string str, int state)
        {
            if(state == (int)ToStates.Deleted || state == (int)ToStates.CompletedDeleted)
                PrintOk(str, false);
            else
                Console.Write(str);
        }

        private static void PrintName(ToDoItem item, bool chosen)
        {
            if (chosen)
                SetChosenColor();
            if (item.Name.Length <= 20)
            {
                SetNameColorAndPrint(item.Name, item.State);
                for (int i = item.Name.Length; i < 19; i++)
                    Console.Write(" ");
                SetDefaultColor();
                return;
            }
            int x = 0;
            while (x < item.Name.Length)
            {
                if (x != 0)
                {
                    if (chosen)
                        SetChosenColor();
                    Console.WriteLine("║ ║");
                    Console.Write("║");
                    SetDefaultColor();
                }
                for (var i = 0; i < 18; i++, x++)
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

            SetDefaultColor();
        }

        private static void PrintMenu()
        {
            string[] menu = new string[] { "Add", "Deleted", "Completed" };
            Console.Clear();
            Console.WriteLine("╔═══╦═══════╦═════════╗");
            Console.Write("║");
            var items = _todo.FindAll(x => x.State == 0 || 
                                         (x.State == (int)ToStates.Completed && _completed) || 
                                         (x.State == (int)ToStates.Deleted && _deleted) || 
                                         (x.State == (int)ToStates.CompletedDeleted && _deleted && _completed));
            _count = items.Count;
            _menuIndexX %= _count + 1;
            for (int i = 0; i < 3; i++)
            {
                if (_menuIndexX == 0 && _menuIndexY == i)
                {
                    PrintChosen(menu[i]);
                    Console.Write("║");
                    continue;
                }
                switch (i)
                {
                    case 0:
                        Console.Write(menu[i]);
                        break;
                    case 1:
                        PrintOk(menu[i], _deleted);
                        break;
                    case 2:
                        PrintOk(menu[i], _completed);
                        break;
                }
                Console.Write("║");
            }
            Console.WriteLine();

            if (_count == 0)
                Console.WriteLine("╚═══╩═══════╩═════════╝");
            else
                Console.WriteLine("╠═══╩═══════╩═══════╦═╣");

            int j = 0;
            foreach (var each in items)
            {
                if (_menuIndexX == j + 1)
                    _item = each;
                Console.Write("║");
                PrintName(each, _menuIndexX == j + 1 && _menuIndexY == 0);
                Console.Write("║");
                string state = "";
                switch (each.State)
                {
                    case (int)ToStates.Deleted:
                        state = "X";
                        break;
                    case (int)ToStates.Uncompleted:
                        state = " ";
                        break;
                    case (int)ToStates.Completed:
                    case (int)ToStates.CompletedDeleted:
                        state = "√";
                        break;
                }
                var isCompleted = each.State == (int)ToStates.Completed || each.State == (int)ToStates.CompletedDeleted;
                if (_menuIndexX == j + 1 && _menuIndexY == 1)
                    PrintChosen(state);
                else
                    PrintOk(state, isCompleted);
                Console.WriteLine("║");
                if (j + 1 == _count)
                    Console.WriteLine("╚═══════════════════╩═╝\nDel to delete item");
                else
                    Console.WriteLine("╠═══════════════════╬═╣");
                j++;
            }
        }

        private static void WorkWithMenu()
        {
            _menuIndexX %= _count + 1;
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.UpArrow:
                    _menuIndexX = (_count + _menuIndexX) % (_count + 1);
                    if (_menuIndexX != 0)
                        _menuIndexY %= 2;
                    break;
                case ConsoleKey.DownArrow:
                    _menuIndexX = (_menuIndexX + 1) % (_count + 1);
                    if (_menuIndexX != 0)
                        _menuIndexY %= 2;
                    break;
                case ConsoleKey.LeftArrow:
                    if (_menuIndexX == 0)
                        _menuIndexY = (_menuIndexY + 2) % 3;
                    else
                        _menuIndexY = (_menuIndexY + 1) % 2;
                    break;
                case ConsoleKey.RightArrow:
                    if (_menuIndexX == 0)
                        _menuIndexY = (_menuIndexY + 1) % 3;
                    else
                        _menuIndexY = (_menuIndexY + 1) % 2;
                    break;
                case ConsoleKey.Enter:
                    EnterClick();
                    break;
                case ConsoleKey.Delete:
                    if (_menuIndexX == 0)
                        break;
                    _item.Delete();
                    if (!_deleted)
                        _count--;
                    break;
            }
        }

        public static void AddClick()
        {
            string name = "";
            while (string.IsNullOrEmpty(name))
            {
                Console.Clear();
                Console.WriteLine("Write new TODO:");
                name = Console.ReadLine();
            }
            _todo.Add(name);
        }

        public static void EnterClick()
        {
            if (_menuIndexX == 0)
            {
                switch (_menuIndexY)
                {
                    case 0:
                        AddClick();
                        break;
                    case 1:
                        _deleted = !_deleted;
                        break;
                    case 2:
                        _completed = !_completed;
                        break;
                }
                return;
            }
            Console.Clear();
            if (_menuIndexX == 0)
                return;
            if (_menuIndexY == 0)
            {
                Console.WriteLine("Write new name for {0}", _item.Name);
                string x = Console.ReadLine();
                while (string.IsNullOrEmpty(x))
                {
                    Console.Clear();
                    Console.WriteLine("Write new name for {0}", _item.Name);
                    x = Console.ReadLine();
                }
                _item.ChangeName(x);
            }
            else
            {
                _item.Complete();
                if (!_completed)
                    _count--;
            }
        }

        private static void Main(string[] args)
        {
            _todo = new ToDoList(Input);
            if (args.Length != 0)
            {
                var argsWorker = new ToDoArgs(_todo);
                argsWorker.WorkWithArgs(args);
                ToDoListReducer.Save(Input, _todo);
                return;
            }
            while (true)
            {
                PrintMenu();
                WorkWithMenu();
                ToDoListReducer.Save(Input, _todo);
            }
        }
    }
}
