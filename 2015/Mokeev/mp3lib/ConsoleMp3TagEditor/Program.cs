using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using mp3lib;

//[TODO] introduce class for args {READY}
//[TODO] extract arguments parsing {READY}

namespace ConsoleMp3TagEditor
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			try
			{
				var argsManager = new ArgsManager(args);
				argsManager.CheckArgsValidity();

				var data = argsManager.ExtactArgs();
				Console.WriteLine(TagType.Album.ToString());
				Mp3File mp3;
				switch (data.Action)
				{
					case ProgramAction.Analyse:
						var pathAnalyser = new Mp3FileAnalyser(Directory.GetFiles(data.Path, "*.mp3"), data.Mask);

                        var differences = pathAnalyser.FindDifferences();
                        foreach (var file in differences)
				        {
				            Console.WriteLine(file);
				        }

                        break;

					case ProgramAction.Mp3Edit:
						mp3 = new Mp3File(data.Path);
						var tagsChanger = new Mp3TagChanger(mp3, data.Mask);
						tagsChanger.ChangeTags();
						break;

					case ProgramAction.FileRename:
						mp3 = new Mp3File(data.Path);
						var renamer = new Mp3FileNameChanger(mp3, data.Mask);
						renamer.ChangeFileName();
						break;
				}

				Console.WriteLine("Done!");

				Console.ReadKey();
			}
			catch (Exception e)
			{
			#if DEBUG
				Console.WriteLine("Exeption: \n{0} \n\nAt:\n{1}",e.Message, e.StackTrace);
			#else
				Console.WriteLine("{0}",e.Message);
			#endif
			}
		}

		

	}
}
