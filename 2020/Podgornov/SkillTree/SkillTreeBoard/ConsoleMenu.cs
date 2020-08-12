using System;
using System.Collections.Generic;
using System.Linq;

namespace SkillTreeBoard
{
    internal class ConsoleMenu<T,TU> where T:IEnumerable<TU>
    {
        private readonly Func<TU, ConsoleColor> _paintInTheAppropriateColor;

        private readonly string _colorDescription;

        private readonly T _collection;

        private readonly Action<TU>[] _actions;
        private readonly bool _total;

        protected string Head { get; set; } =
            "****************" +
            "\nQ - exit" +
            "\nUpArrow and DownArrow - up and down" +
            "\nEnter - choose" +
            "\n****************";

        private int _index;

        private int EndPosition => Count + _topPosition;

        private int IndexPosition => _index + _topPosition;

        private  int _topPosition;

        public int Count => _collection.Count();

        public int SelectedItemsIndex
        {
            get => _index;
            set
            {
                if (Count == 0) return; 
                Console.SetCursorPosition(0, IndexPosition);
                SimplePrint((_collection.ElementAt(_index)));
                _index = value < 0 ? Count + value : value % Count;
                Console.SetCursorPosition(0, IndexPosition);
                PrintStringOnFocus((_collection.ElementAt(_index).ToString()));
            } 
        }

        public ConsoleMenu(T collection, Action<TU>[] actions,bool total = false)
        {
            _collection = collection;
            _actions = actions;
            _total = total;
        }

        public ConsoleMenu(T collection, Action<TU>[] actions,
            Func<TU,ConsoleColor> paintInTheAppropriateColor , string colorDescription , bool total = false):this(collection, actions, total)
        {
            _paintInTheAppropriateColor = paintInTheAppropriateColor;
            _colorDescription = colorDescription;
        }

        public void Start()
        {
            InitializeMenu();
            var exitFlag = false;
            while (true)
            {
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        SelectedItemsIndex--;
                        break;
                    case ConsoleKey.DownArrow:
                        SelectedItemsIndex++;
                        break;
                    case ConsoleKey.Enter:
                        if (_actions != null && _actions.Length != 0)
                            _actions[SelectedItemsIndex].Invoke(_collection.ElementAt(SelectedItemsIndex));
                        InitializeMenu();
                        break;
                    case ConsoleKey.Q:
                        exitFlag = true;
                        break;
                }
                ClearConsoleInput();
                if (exitFlag)
                {
                    break;
                }
            }
        }

        private void InitializeMenu()
        {
            PrintHead();
            PrintMenu();
        }

        private void PrintMenu()
        {
            for (int i = 0; i < Count; i++)
            {
                if (i == _index)
                {
                    PrintStringOnFocus(_collection.ElementAt(i).ToString());
                    Console.WriteLine();
                }
                else
                {
                    SimplePrint(_collection.ElementAt(i));
                    Console.WriteLine();
                }
            }
        }

        private void PrintHead()
        {
            Console.Clear();
            Console.WriteLine(Head);
            if (_paintInTheAppropriateColor != null)
            {
                Console.WriteLine(_colorDescription);
            }

            if (_total)
                Console.WriteLine($"total : {_collection.Count()}");
            _topPosition = Console.CursorTop;
            Console.CursorVisible = false;
        }

        private void PrintStringOnFocus(string str)
        {
            var tempBackgroundColor = Console.BackgroundColor;
            var tempForegroundColor = Console.ForegroundColor;
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(str);
            Console.BackgroundColor = tempBackgroundColor;
            Console.ForegroundColor = tempForegroundColor;
        }

        private void SimplePrint(TU item)
        {
            if (_paintInTheAppropriateColor != null)
            {
                var consoleColor = _paintInTheAppropriateColor(item);
                var temp = Console.ForegroundColor;
                Console.ForegroundColor = consoleColor;
                Console.Write(item.ToString());
                Console.ForegroundColor = temp;
            }
            else
            {
                Console.Write(item.ToString());
            }
        }

        private void ClearConsoleInput()
        {
            Console.SetCursorPosition(0, EndPosition);
            Console.Write(" ");
            Console.SetCursorPosition(0, EndPosition);
        }
    }
}
