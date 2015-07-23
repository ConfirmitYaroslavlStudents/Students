using System;
using System.Collections.Generic;
using mp3_lib;

namespace ConsoleMp3TagEditor
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			if (!ArgsValid(args)) throw new ArgumentException("Wrong arguments passed", "args");

			var data = ExtactArgs(args);

			var mp3 = new Mp3File(data["-file"]);

			var renamer = new Mp3TagChanger(mp3);

			renamer.Start(data["-mask"]);

			Console.WriteLine("Done!");

			Console.ReadKey();
		}

		public static bool ArgsValid(string[] args)
		{
			if (args.Length != 4)
			{
				Console.WriteLine("Expected usage: {0} -file \"[path to file]\" -mask \"[mask for changing title]\"");
				return false;
			}

			var hasFilePath = false;
			var hasMask = false;
			foreach (var str in args)
			{
				switch (str)
				{
					case "-file":
						hasFilePath = true;
						break;
					case "-mask":
						hasMask = true;
						break;
				}
			}

			var hasValidArgs = hasMask && hasFilePath;
			if (!hasValidArgs)
			{
				if (!hasMask && !hasFilePath)
				{
					Console.WriteLine("You don't append correct file path and mask for mp3.");
				}
				else if (!hasFilePath)
				{
					Console.WriteLine("You don't append correct file path.");
				}
				else
				{
					Console.WriteLine("You don't append correct mask for mp3.");
				}

				return false;
			}

			if (args[0] == "-file" && args[1] == "-mask" || args[0] == "-mask" && args[1] == "-file")
			{
				Console.WriteLine("You don't append correct file path and mask for mp3.");
				return false;
			}

			return true;
		}

		public static Dictionary<string, string> ExtactArgs(IEnumerable<string> args)
		{
			var q = new Queue<string>();
			var retDict = new Dictionary<string, string>();
			foreach (var arg in args)
			{
				if (q.Count == 0)
				{
					q.Enqueue(arg);
					continue;
				}

				retDict.Add(q.Dequeue(), arg);
			}

			return retDict;
		}

	}
}
