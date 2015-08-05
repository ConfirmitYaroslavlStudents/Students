using System;
using System.Collections.Generic;
using System.IO;
using Mp3Handler;

namespace MP3tager
{
    class Program
    {
        static void Main(string[] args)
        {
            //todo: include try-catch into mode methods
            try
            {
                if (args.Length >= 1)
                {
                    switch (args[0])
                    {
                        case "help":
                            Console.WriteLine(Messeges.LongHelp);
                            break;
                        case "retag":
                            LaunchRetagMode(args);
                            break;
                        case "rename":
                            LaunchRenameMode(args);
                            break;
                        //case "synch":
                        //    LaunchSycnhMode(args);
                        //    break;
                            //todo: imlement difference
                        default:
                            Console.WriteLine(Messeges.InvalidFirsArg);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine(Messeges.ShortHelp);
                }
            }
            catch(FileNotFoundException)
            {
                Console.WriteLine(Messeges.FileNotExist);
            }
            catch(KeyNotFoundException)
            {
                Console.WriteLine(Messeges.KeyNotFound);
            }
            
        }

        private static void LaunchSycnhMode(string[] args)
        {
            switch (args.Length)
            {
                case 3:
                    var retager = new Mp3FileProcessor(args[1]);
                    retager.Synchronize(args[2]);
                    break;
                default:
                    Console.WriteLine(Messeges.RenamemodeHelp);
                    break;
            }
        }

        private static void LaunchRenameMode(string[] args)
        {
            switch (args.Length)
            {
                case 3:
                    var retager = new Mp3FileProcessor(args[1]);
                    retager.RenameFile(args[2]);
                    break;
                default:
                    Console.WriteLine(Messeges.RenamemodeHelp);
                    break;
            }
        }

        private static void LaunchRetagMode(string[] args)
        {
            switch (args.Length)
            {
                case 3:
                    var retager = new Mp3FileProcessor(args[1]);
                    retager.RetagFile(args[2]);
                    break;
                default:
                    Console.WriteLine(Messeges.RetagmodeHelp);
                    break;
            }
        }
    }
}
