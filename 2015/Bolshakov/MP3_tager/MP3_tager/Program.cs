using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using RetagerLib;

namespace MP3_tager
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
                            RetagFile(args);
                            break;
                        case "rename":
                            RenameFile(args);
                            break;
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
            catch(NotValidPatternException)
            {
                Console.WriteLine(Messeges.NotValidPatter);
            }
            catch(KeyNotFoundException)
            {
                Console.WriteLine(Messeges.KeyNotFound);
            }
            
        }

        private static void RenameFile(string[] args)
        {
            throw new NotImplementedException();
        }

        private static void RetagFile(string[] args)
        {
            switch (args.Length)
            {
                case 3:
                    var retager = new Retager();
                    retager.TagFile(args[1],args[2]);
                    break;
                default:
                    Console.WriteLine(Messeges.RetagmodeHelp);
                    break;
            }
        }
    }
}
