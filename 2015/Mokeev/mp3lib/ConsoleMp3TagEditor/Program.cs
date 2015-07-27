using System;
using System.Collections.Generic;
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

				//[TODO] introduce class for args {READY}
				//[TODO] extract arguments parsing {READY}
				var mp3 = new Mp3File(data.FilePath);

				var renamer = new Mp3TagChanger(mp3, data.Mask);

				renamer.ChangeTags();

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
