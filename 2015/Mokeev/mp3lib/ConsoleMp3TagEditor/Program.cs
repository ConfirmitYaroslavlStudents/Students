using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using mp3lib;

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
                        var mp3Files = Directory.GetFiles(data.Path, "*.mp3").Select(file => new Mp3File(file)).Cast<IMp3File>().ToArray();
				        var pathAnalyser = new Mp3FileAnalyser(mp3Files, data.Mask);

                        var differences = pathAnalyser.FindDifferences();
                        foreach (var file in differences)
				        {
				            Console.WriteLine("This file has differences: {0}", file.File);
				            foreach (var diff in file.Diffs)
				            {
                                Console.WriteLine("{0} in File Name: {1}", diff.Key, diff.Value.FileNameValue);
                                Console.WriteLine("{0} in Tags: {1}", diff.Key, diff.Value.TagValue);
                            }
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
