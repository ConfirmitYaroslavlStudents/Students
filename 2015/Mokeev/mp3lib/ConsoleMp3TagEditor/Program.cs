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
				Mp3File mp3;
				IEnumerable<IMp3File> mp3Files;
				switch (data.Action)
				{
					case ProgramAction.Analyse:
						mp3Files = Directory.GetFiles(data.Path, "*.mp3").Select(file => new Mp3File(file));
						var pathAnalyser = new Mp3FileAnalyser(mp3Files, data.Mask);

						var differences = pathAnalyser.GetDifferences();
						new CommunicationWithUser().ShowDifferences(differences, new ConsoleCommunication());

						break;

					case ProgramAction.Mp3Edit:
						mp3 = new Mp3File(data.Path);
						var tagsChanger = new Mp3TagChanger(mp3, data.Mask);
						tagsChanger.ChangeTags();
						break;

					case ProgramAction.FileRename:
						mp3 = new Mp3File(data.Path);
						var renamer = new Mp3FileNameChanger(mp3, data.Mask);
						var newFileName = renamer.GetNewFileName();
						mp3.ChangeFileName(newFileName);
						break;

					case ProgramAction.Sync:
						mp3Files = Directory.GetFiles(data.Path, "*.mp3").Select(file => new Mp3File(file));
						var syncer = new Mp3Syncing(mp3Files, data.Mask, new ConsoleCommunication());
						syncer.SyncFiles();

						break;

				}

				Console.WriteLine("Done!");

			}
			catch (Exception e)
			{
/*#if DEBUG
				Console.WriteLine("Exeption: \n{0} \n\nAt:\n{1}", e.Message, e.StackTrace);
#else
				*/
				Console.WriteLine("Error occured: {0}", e.Message);
//#endif
			}
			finally
			{
				Console.ReadKey();
			}
		}



	}
}
