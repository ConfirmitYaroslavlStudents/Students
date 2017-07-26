using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logger;
using RenamerLib;

namespace MP3Renamer
{
    //public class Renamer
    //{
    //    public string directory;

    //    public Renamer(string directory)
    //    {
    //        this.directory = directory;
    //    }

    //    public void Rename(string mask, bool isRecursive, bool isTagToFileName)
    //    {
            
    //        string[] files = GetFiles(mask, isRecursive);
    //        if (files.Length == 0)
    //            Console.WriteLine("No files!");
            
    //        if(isTagToFileName)
    //            RenameFileFromTag(files);
    //        else
    //            ChangeTagFromFile(files);
    //    }

    //    public string[] GetFiles(string mask, bool isRecursive) => 
    //        Directory.GetFiles(directory, mask, 
    //            isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

    //    private void RenameFileFromTag(string[] files)
    //    {
    //        foreach (var file in files)
    //        {
    //            var taggedFile = TagLib.File.Create(file);
    //            var artist = taggedFile.Tag.FirstPerformer;
    //            var name = taggedFile.Tag.Title;
    //            var newFile = Path.GetDirectoryName(file) + "\\" + artist + " - " + name + ".mp3";
    //            if (!File.Exists(newFile))
    //                File.Move(file, newFile);
    //            Console.WriteLine(file + " -> " + newFile);
    //        }
    //    }

    //    private void ChangeTagFromFile(string[] files)
    //    {
    //        foreach (var file in files)
    //        {
    //            var filename = Path.GetFileName(file);
    //            var artist = filename.Substring(0, filename.IndexOf(" - "));
    //            int position = filename.IndexOf(" - ") + " - ".Length,
    //                length = filename.IndexOf(".mp3") - position;
    //            var name = filename.Substring(position, length);

    //            var taggedFile = TagLib.File.Create(file);
    //            taggedFile.Tag.Performers = new string[] { artist };
    //            taggedFile.Tag.Title = name;
    //            taggedFile.Save();
    //        }
    //    }
    //}

    public class ProcessExecutor
    {
        public void Execute(string[] args)
        {
            TextFileLogger logger = new TextFileLogger("log.txt");
            ArgumentsParser parser = new ArgumentsParser();
            Arguments arguments = null;
            try
            {
                arguments = parser.ParseArguments(args);
            }
            catch (ArgumentException e)
            {
                logger.WriteMessage(e.Message, Status.Error);
                Console.WriteLine("Process failed! See log.txt for full information");
                logger.StopLogging();
                return;
            }

            try
            {
                Processor processor = new Processor(arguments, logger);
                processor.Process();
            }
            catch (Exception e)
            {
                logger.WriteMessage(e.Message, Status.Error);
                Console.WriteLine("Process failed! See log.txt for full information");
            }

            logger.StopLogging();
        }
    }
}
