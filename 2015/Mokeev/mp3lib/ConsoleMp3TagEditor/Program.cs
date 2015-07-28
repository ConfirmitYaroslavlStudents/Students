using System;
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

				var mp3 = new Mp3File(data.Path);

				switch (data.Action)
				{
					case ProgramAction.Analyse:


						break;

					case ProgramAction.Mp3Edit:
						var tagsChanger = new Mp3TagChanger(mp3, data.Mask);
						tagsChanger.ChangeTags();
						break;

					case ProgramAction.FileRename:
						var renamer = new Mp3TagChanger(mp3, data.Mask);
						renamer.ChangeTags();
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
