using System;
using System.Text;

namespace MyTODO
{
    public class ToDoArgs
    {
        private readonly ToDoList _todo;

        public ToDoArgs(ToDoList todolist)
        {
            _todo = todolist;
        }

        int GetAndCheckInt(string[] args, ref int index)
        {
            if (args.Length <= index + 1)
            {
                Console.WriteLine("insufficient number of arguments");
                return -1;
            }
            index++;
            if (int.TryParse(args[index], out var i) && i >= 0 && i < _todo.Count)
                return i;
            else
                Console.WriteLine("argument must be integer >= 0 and less than number of items");
            return -1;
        }

        string GetString(string[] args, ref int index)
        {
            index++;
            var bufIndex = index;
            if (args.Length <= index || args[index][0] != '\"')
                return "";
            var result = new StringBuilder(args[index++].Remove(0, 1));
            result.Append(' ');
            while (index < args.Length && !args[index].EndsWith('\"'))
            {
                result.Append(args[index++]);
                result.Append(' ');
            }
            if (index >= args.Length)
            {
                Console.WriteLine("not found end of name");
                index = bufIndex;
                return "";
            }
            result.Append(args[index].Remove(args[index].Length - 1, 1));
            return result.ToString();
        }

        public void PrintHelp()
        {
            Console.WriteLine("Usage:\nMyTODO.exe [add \"[name]\"|\n" +
                              "changename [index] \"[name]\"|\n" +
                              "complete [index]|\n" +
                              "delete [index]|\n" +
                              "show [index]]\n" +
                              "Parameters:\n" +
                              "add        | -a  add new task to ToDo list with specified name\n" +
                              "changename | -cn changes name to task with specified index\n" +
                              "complete   | -co setting completed status to task with specified index\n" +
                              "delete     | -d  setting deleting status to task with specified index\n" +
                              "show       | -s  shows task with specified index\n");
        }

        void AddItem(string[] args, ref int index)
        {
            var name = GetString(args, ref index);
            _todo.Add(name);
            Console.WriteLine("item \"{0}\" added successfully", name);
        }

        void ChangeName(string[] args, ref int index)
        {

            if (args.Length <= index + 2)
            {
                Console.WriteLine("insufficient number of arguments");
                return;
            }
            var i = GetAndCheckInt(args, ref index);
            if (i < 0)
                return;
            var name = GetString(args, ref index);
            _todo[i].ChangeName(name);
            Console.WriteLine("item's \"{0}\" name changed successfully", _todo[i].Name);
        }

        void CompleteTask(string[] args, ref int index)
        {
            var i = GetAndCheckInt(args, ref index);
            if (i < 0)
                return;
            _todo[i].Complete();
            Console.WriteLine("Item \"{0}\" completed successfully", _todo[i].Name);
        }

        void DeleteTask(string[] args, ref int index)
        {
            var i = GetAndCheckInt(args, ref index);
            if (i < 0)
                return;
            _todo[i].Delete();
            Console.WriteLine("Item \"{0}\" deleted successfully", _todo[i].Name);
        }

        void ShowTask(string[] args, ref int index)
        {
            var i = GetAndCheckInt(args, ref index);
            if (i < 0)
                return;
            Console.WriteLine(_todo[i]);
        }

        public void WorkWithArgs(string[] args)
        {
            for (var i = 0; i < args.Length; i++)
            {
                switch (args[i].ToLower())
                {
                    case "help":
                        PrintHelp();
                        break;
                    case "add":
                    case "-a":
                        AddItem(args, ref i);
                        break;
                    case "changename":
                    case "-cn":
                        ChangeName(args, ref i);
                        break;
                    case "complete":
                    case "-co":
                        CompleteTask(args, ref i);
                        break;
                    case "delete":
                    case "-d":
                        DeleteTask(args, ref i);
                        break;
                    case "show":
                    case "-s":
                        ShowTask(args, ref i);
                        break;
                    default:
                        Console.WriteLine("unknown command: {0}, type \"help\" to see list of commands", args[0]);
                        break;
                }
            }
        }
    }
}
