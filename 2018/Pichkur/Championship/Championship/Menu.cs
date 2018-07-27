using System;
using System.Collections.Generic;

namespace Championship
{
    public class Menu
    {
        private readonly List<MenuItem> _menuItems;
        private readonly int _length;

        public Menu(List<MenuItem> menuItems, string appTitle)
        {
            _menuItems = menuItems;
            _menuItems.Add(new MenuItem(null, "Back/Exit"));

            _length = _menuItems.Count;

            Console.Title = appTitle;
        }

        public void Start()
        {
            Console.Clear();
            int old = 0;
            int current = old;
            bool rewrite = true;
            ConsoleKeyInfo k;
            Print(old, current, rewrite);
            rewrite = false;

            while (true)
            {
                k = Console.ReadKey();
                switch (k.Key)
                {
                    case ConsoleKey.UpArrow:
                        current = current > 0 ? current - 1 : _length - 1;
                        break;

                    case ConsoleKey.DownArrow:
                        current = current < _length - 1 ? current + 1 : 0;
                        break;

                    case ConsoleKey.Enter:
                        {
                            if (current == _length - 1)
                            {
                                return;
                            }

                            Console.Clear();

                            _menuItems[current].Action.Invoke();

                            rewrite = true;
                        }
                        break;

                    case ConsoleKey.Escape:
                        {
                            return;
                        }
                }
                Print(old, current, rewrite);

                if (rewrite)
                {
                    rewrite = false;
                }

                old = current;
            }
        }

        private void Print(int old, int current, bool firstOrNot)
        {
            if (firstOrNot)
            {
                Console.Clear();

                for (int i = 0; i < _length; i++)
                {
                    Console.ForegroundColor = current == i ? ConsoleColor.Green : ConsoleColor.Gray;
                    Console.WriteLine(_menuItems[i].Title);
                }
            }
            else
            {
                if (old != current)
                {
                    Console.SetCursorPosition(0, current);
                    Console.ForegroundColor = current != _length - 1 ? ConsoleColor.Green : ConsoleColor.Red;
                    Console.WriteLine(_menuItems[current].Title);

                    Console.SetCursorPosition(0, old);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine(_menuItems[old].Title);
                }
            }

            Console.SetCursorPosition(0, _length);
            Console.Write(" ");
            Console.SetCursorPosition(0, _length);
        }
    }
}
