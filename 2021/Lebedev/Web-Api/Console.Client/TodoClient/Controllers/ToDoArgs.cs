using System;
using System.Text;
using MyTODO;

namespace ToDoClient.Controllers
{
    public class ToDoArgs
    {
        private readonly IToDoConnector _todo;

        public ToDoArgs(IToDoConnector todolist)
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
            if (int.TryParse(args[index], out var i) && i >= 0 && i < _todo.FindAll(true,true).Count)
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
                Console.WriteLine("not found end of Name");
                index = bufIndex;
                return "";
            }
            result.Append(args[index].Remove(args[index].Length - 1, 1));
            return result.ToString();
        }

        public void PrintHelp()
        {
            Console.WriteLine("Usage:\nMyTODO.exe [add \"[Name]\"|\n" +
                              "changename [index] \"[Name]\"|\n" +
                              "complete [index]|\n" +
                              "delete [index]|\n" +
                              "show [index]]\n" +
                              "Parameters:\n" +
                              "add        | -a  add new task to ToDo list with specified Name\n" +
                              "changename | -cn changes Name to task with specified index\n" +
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
            _todo.ChangeName(i, name);
            Console.WriteLine("item's \"{0}\" Name changed successfully", _todo.GetItem(i).Name);
        }

        void CompleteTask(string[] args, ref int index)
        {
            var i = GetAndCheckInt(args, ref index);
            if (i < 0)
                return;
            _todo.Complete(i);
            Console.WriteLine("Item \"{0}\" completed successfully", _todo.GetItem(i).Name);
        }

        void DeleteTask(string[] args, ref int index)
        {
            var i = GetAndCheckInt(args, ref index);
            if (i < 0)
                return;
            _todo.Delete(i);
            Console.WriteLine("Item \"{0}\" Deleted successfully", _todo.GetItem(i).Name);
        }

        void ShowTask(string[] args, ref int index)
        {
            var i = GetAndCheckInt(args, ref index);
            if (i < 0)
                return;
            Console.WriteLine(_todo.GetItem(i));
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
