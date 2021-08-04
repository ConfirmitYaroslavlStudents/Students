using System;

namespace MyTODO.Controllers
{
    public class ToDoMenu
    {
        private bool _deleted;
        private bool _completed = true;
        private IToDoConnector _todo;
        private int _chosenid;
        private int _count;
        private int _menuIndexX;
        private int _menuIndexY;

        public ToDoMenu(IToDoConnector todolist)
        {
            _todo = todolist;
        }

        private void SetDefaultColor()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void SetChosenColor()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
        }

        private void PrintChosen(string str)
        {
            SetChosenColor();

            Console.Write(str);

            SetDefaultColor();
        }

        private void PrintOk(string str, bool ok)
        {
            if (ok)
                Console.ForegroundColor = ConsoleColor.Green;
            else
                Console.ForegroundColor = ConsoleColor.Red;

            Console.Write(str);

            Console.ForegroundColor = ConsoleColor.White;
        }

        private void PrintItemPart(string part, bool completed, bool deleted)
        {
            if (!completed && !deleted)
                Console.Write(part);
            else
                PrintOk(part, !deleted);
        }

        private void PrintName(ToDoItem item, bool chosen)
        {
            Console.Write("║");
            if (chosen)
                SetChosenColor();
            if (item.name.Length <= 20)
            {
                PrintItemPart(item.name, item.completed, item.deleted);
                for (var i = item.name.Length; i < 19; i++)
                    Console.Write(" ");
                SetDefaultColor();
                return;
            }
            var x = 0;
            while (x < item.name.Length)
            {
                if (x != 0)
                {
                    Console.WriteLine("║ ║");
                    Console.Write("║");
                }
                if (chosen)
                    SetChosenColor();
                for (var i = 0; i < 18; i++, x++)
                    if (x < item.name.Length)
                        PrintItemPart(item.name[x].ToString(), item.completed, item.deleted);
                    else
                        Console.Write(" ");
                if (x < item.name.Length)
                    if (item.name[x] != ' ')
                        PrintItemPart("-", item.completed, item.deleted);
                    else
                        PrintItemPart(item.name[x++].ToString(), item.completed, item.deleted);
                else
                    Console.Write(" ");
                SetDefaultColor();
            }

            SetDefaultColor();
        }

        public void PrintMenu()
        {
            var items = _todo.FindAll(_completed, _deleted);
            PrintMenuHeader(items);
            PrintMenuBody(items);
        }

        void PrintMenuHeader(ToDoList items)
        {
            var menu = new[] { "Add", "Deleted", "Completed" };
            Console.Clear();
            Console.Write("╔═══╦═══════╦═════════╗\n║");
            _count = items.Count;
            _menuIndexX %= _count + 1;
            for (var i = 0; i < 3; i++)
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
        }

        void PrintMenuBody(ToDoList items)
        {
            var j = 0;
            foreach (var each in items)
            {
                if (_menuIndexX == j + 1)
                    _chosenid = each.id;
                PrintName(each, _menuIndexX == j + 1 && _menuIndexY == 0);
                var state = " ";
                if (each.completed)
                    state = "√";
                else
                    if (each.deleted)
                    state = "X";
                Console.Write("║");
                if (_menuIndexX == j + 1 && _menuIndexY == 1)
                    PrintChosen(state);
                else
                    PrintOk(state, each.completed);
                Console.WriteLine("║");
                if (j + 1 == _count)
                    Console.WriteLine("╚═══════════════════╩═╝\nDel to delete item");
                else
                    Console.WriteLine("╠═══════════════════╬═╣");
                j++;
            }
        }

        public void WorkWithMenu()
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
                    _todo.Delete(_chosenid);
                    if (!_deleted)
                        _count--;
                    break;
            }
        }

        public void AddClick()
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

        public void EnterClick()
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
                Console.WriteLine("Write new name for {0}", _todo.GetItem(_chosenid).name);
                var x = Console.ReadLine();
                while (string.IsNullOrEmpty(x))
                {
                    Console.Clear();
                    Console.WriteLine("Write new name for {0}", _todo.GetItem(_chosenid).name);
                    x = Console.ReadLine();
                }
                _todo.ChangeName(_chosenid, x);
            }
            else
            {
                _todo.Complete(_chosenid);
                if (!_completed)
                    _count--;
            }
        }
    }
}
