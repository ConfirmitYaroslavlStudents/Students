using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace MP3Renamer
{
    public class Program
    {
        public static void ParseArguments(string[] args, out string mask, out bool isRecursive, out bool isTagToFileName)
        {
            if (args.Length < 2 || args.Length > 3)
                throw new ArgumentException("Wrong number of parameters provided!");

            mask = args[0];
            if (mask.IndexOf(".mp3", StringComparison.Ordinal) != mask.Length - 4)
                throw new ArgumentException("Wrong mask provided! Should be mask for .mp3\nExample: *.mp3");

            isRecursive = String.Equals(args[1], "-recursive");
            isTagToFileName = String.Equals(isRecursive ? args[2] : args[1], "-toFileName");
        }

        public static void Main(string[] args)
        {
            var directory = Directory.GetCurrentDirectory();
            string mask;
            bool isRecursive, isTagToFileName;
            try
            {
                ParseArguments(args, out mask, out isRecursive, out isTagToFileName);
                Renamer renamer = new Renamer(directory);
                renamer.Rename(mask, isRecursive, isTagToFileName);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Usage: mp3renamer.exe <mask> [-recursive] (-toFileName | -toTag)");
            }

            
        }
    }
}
