using System;
using System.Text;

namespace MyTODO
{
    public class ToDoArgs
    {
        readonly ToDoList _todo;

        public ToDoArgs(ToDoList list)
        {
            _todo = list;
        }

        int GetIndex(string arg)
        {
            if (int.TryParse(arg, out var index) && index >= 0)
                return index;
            else
                Console.WriteLine("argument must be integer >= 0");
            return -1;
        }

        string GetName(string[] args, ref int index)
        {
            var bufIndex = index;
            if (args.Length <= index || args[index][0] != '*')
                return "";
            var result = new StringBuilder(args[index++].Remove(0, 1));
            result.Append(' ');
            while (index < args.Length && !args[index].EndsWith('*'))
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

        public void WorkWithArgs(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i].ToLower())
                {
                    case "help":
                        Console.WriteLine("Usage:\nMyTODO.exe [add *[name]*|\n" +
                            "changename [index] *[name]*|\n" +
                            "complete [index]|\n" +
                            "delete [index]|\n" +
                            "show [index]]\n" +
                            "Parameters:\n" +
                            "add        | -a  add new task to ToDo list with specified name\n" +
                            "changename | -cn changes name to task with specified index\n" +
                            "complete   | -co setting completed status to task with specified index\n" +
                            "delete     | -d  setting deleting status to task with specified index\n" +
                            "show       | -s  shows task with specified index\n");
                        break;
                    case "add":
                    case "-a":
                        i++;
                        var name = GetName(args, ref i);
                        _todo.Add(name);
                        Console.WriteLine("item \"{0}\" added successfully", name);
                        break;
                    case "changename":
                    case "-cn":
                        if (args.Length <= i + 2)
                        {
                            Console.WriteLine("insufficient number of arguments");
                            return;
                        }
                        var index = GetIndex(args[i + 1]);
                        if (index < 0 || index >= _todo.Count)
                        {
                            Console.WriteLine("argument must be integer >= 0 and less than number of items");
                            break;
                        }
                        i += 2;
                        name = GetName(args, ref i);
                        _todo[index].ChangeName(name);
                        Console.WriteLine("item's \"{0}\" name changed successfully", _todo[index].Name);
                        break;
                    case "complete":
                    case "-co":
                        if (args.Length <= i + 1)
                        {
                            Console.WriteLine("insufficient number of arguments");
                            return;
                        }
                        index = GetIndex(args[i + 1]);
                        if (index < 0 || index >= _todo.Count)
                        {
                            Console.WriteLine("argument must be integer >= 0 and less than number of items");
                            break;
                        }
                        _todo[index].Complete();
                        Console.WriteLine("Item \"{0}\" completed successfully", _todo[index].Name);
                        i++;
                        break;
                    case "delete":
                    case "-d":
                        if (args.Length <= i + 1)
                        {
                            Console.WriteLine("insufficient number of arguments");
                            return;
                        }
                        index = GetIndex(args[i + 1]);
                        if (index < 0 || index >= _todo.Count)
                        {
                            Console.WriteLine("argument must be integer >= 0 and less than number of items");
                            break;
                        }
                        _todo[index].Delete();
                        Console.WriteLine("Item \"{0}\" deleted successfully", _todo[index].Name);
                        i++;
                        break;
                    case "show":
                    case "-s":
                        if (args.Length <= i + 1)
                        {
                            Console.WriteLine("insufficient number of arguments");
                            return;
                        }
                        index = GetIndex(args[1]);
                        if (index < 0 || index >= _todo.Count)
                        {
                            Console.WriteLine("argument must be integer >= 0 and less than number of items");
                            break;
                        }
                        Console.WriteLine(_todo[index]);
                        i++;
                        break;
                    default:
                        Console.WriteLine("unknown command: {0}, type \"help\" to see list of commands", args[0]);
                        break;
                }
            }
        }
    }
}
