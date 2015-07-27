using System;
using System.Collections.Generic;
using System.IO;
using RetagerLib;

namespace MP3_tager
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                switch (args.Length)
                {
                    case 1:
                        Console.WriteLine(args[0] == "help" ? Messeges.LongHelp : Messeges.ShortHelp);
                        break;
                    case 2:
                        var tager = new Retager();
                        tager.TagFile(args[0], args[1]);
                        break;
                    default:
                        Console.WriteLine(Messeges.ShortHelp);
                        break;
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
    }
}
