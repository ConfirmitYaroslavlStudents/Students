using System;
using System.Collections.Generic;
using System.IO;
using Mp3Handler;
using FolderLib;

namespace MP3tager
{
    class Program
    {
        static void Main(string[] args)
        {
            //todo: include try-catch into mode methods
            try
            {
                if( args.Length == 1 && 
                    args[0] == "help")
                    Console.WriteLine(Messeges.LongHelp);
                else if (args.Length == 3)
                {
                    var mode = args[0];
                    var path = args[1];
                    var pattern = args[2];
                    switch (mode)
                    {
                        case "diff":
                            LaunchDiffMode(path,pattern);
                            break;
                        case "retag":
                            LaunchRetagMode(path,pattern);
                            break;
                        case "rename":
                            LaunchRenameMode(path, pattern);
                            break;
                        case "synch":
                            LaunchSynchMode(path,pattern);
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
            catch(KeyNotFoundException)
            {
                Console.WriteLine(Messeges.KeyNotFound);
            }
            
        }

        private static void LaunchSynchMode(string path, string pattern)
        {
            var folderProcessor = new FolderProcessor();
            folderProcessor.PrepareSynch(path, pattern);
            Console.WriteLine(Messeges.FilesToRename);
            foreach (var renameAction in folderProcessor.LateFileHandler.ToRename)
            {
                Console.WriteLine("\t\"{0}\" to \t\"{1}\"",renameAction.FilePath,renameAction.NewName);
            }
            Console.WriteLine(Messeges.FilesToTag);
            foreach (var retagAction in folderProcessor.LateFileHandler.ToRetag)
            {
                Console.WriteLine("\t{0} new tags:",retagAction.FilePath);
                foreach (var tag in retagAction.NewTags)
                {
                    Console.WriteLine("{0}: \"{1}\"",Frame.GetString(tag.Key),tag.Value);
                }
            }
            Console.WriteLine("OK? y/n");
            if (Console.ReadLine() == "y")
                folderProcessor.CompleteSych(new int[0]);
        }

        private static void LaunchDiffMode(string path, string pattern)
        {
            var folderProcessor = new FolderProcessor();
            var differences = folderProcessor.GetDifferences(path,pattern);
            foreach (var difference in differences)
            {
                if (difference.Value.Count != 0)
                {
                    Console.WriteLine("{0} differences:",difference.Key);
                    foreach (var value in difference.Value)
                    {
                        Console.WriteLine("\t{0} in file: \"{1}\", in name: \"{2}\"", Frame.GetString(value.Key), value.Value.FileValue, value.Value.PathValue);
                    }
                }
            }
        }

        private static void LaunchRenameMode(string path, string pattern)
        {
            var retager = new Mp3FileProcessor(path);
            retager.RenameFile(pattern);
        }

        private static void LaunchRetagMode(string path, string pattern)
        {
            var retager = new Mp3FileProcessor(path);
            retager.RetagFile(pattern);
        }
    }
}
