using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuForConsole
{
    public class Menu
    {
        private string[] _menu;
        private Action[] _actions;

        public Menu(string[] menu, Action[] actions)
        {
            if (menu.Length != actions.Length)
            {
                throw new ArgumentException("menu.Length != actions.Length");
            }
            else
            {
                _menu = menu;
                _actions = actions;
            }
        }

        public void UseIt()
        {
            Console.Clear();
            int old = 0;
            int current = old;
            bool rewrite = true;
            ConsoleKeyInfo k;
            PrintSomeMenu(old, current, rewrite);
            rewrite = false;

            while (true)
            {
                k = Console.ReadKey();
                switch (k.Key)
                {
                    case ConsoleKey.UpArrow:

                        if (current > 0)
                        {
                            current--;
                        }
                        else
                        {
                            current = _menu.Length - 1;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (current < _menu.Length - 1)
                        {
                            current++;
                        }
                        else
                        {
                            current = 0;
                        }
                        break;
                    case ConsoleKey.Enter:
                        {
                            Console.Clear();
                            _actions[current].Invoke();
                            Console.ReadLine();
                            rewrite = true;
                        }
                        break;
                    case ConsoleKey.Escape:
                        {
                            return;
                        }
                }
                PrintSomeMenu(old, current, rewrite);
                if (rewrite)
                {
                    rewrite = false;
                }
                old = current;
            }
        }

        private void PrintSomeMenu(int old, int current, bool firstOrNot)
        {
            if (firstOrNot)
            {
                Console.Clear();
                for (int i = 0; i < _menu.Length; i++)
                {
                    if (i == current)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    Console.WriteLine(_menu[i]);
                }
            }
            else
            {
                if (old != current)
                {
                    Console.SetCursorPosition(0, current);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(_menu[current]);

                    Console.SetCursorPosition(0, old);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine(_menu[old]);
                }
            }

            Console.SetCursorPosition(0, _menu.Length);
            Console.Write(" ");
            Console.SetCursorPosition(0, _menu.Length);
        }
    }
}
