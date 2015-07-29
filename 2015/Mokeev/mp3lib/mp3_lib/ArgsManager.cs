using System;
using System.Collections.Generic;
using System.Linq;

namespace mp3lib
{
	public class ArgsManager
	{
		private string[] Args { get; set; }

		public ArgsManager(string[] args)
		{
			Args = args;
		}

		/// <summary>
		/// Validate args for mp3 file
		/// </summary>
		/// <returns>true or throws an Exeptions</returns>
		/// <exception cref="ArgumentException">Wrong argument</exception>
		public bool CheckArgsValidity()
		{
			if (Args.Length != 6)
			{
				throw new ArgumentException("Expected usage: \n\t-action [analyse|file-rename|change-tags] -path \"[path to file]\" -mask \"[mask for changing title]\" \n\tor \n\t-");
			}

			var hasFilePath = false;
			var hasMask = false;
			var setAction = false;
			foreach (var arg in Args)
			{
				switch (arg)
				{
					case "-path":
						hasFilePath = true;
						break;
					case "-mask":
						hasMask = true;
						break;
					case "-action":
						setAction = true;
                        break;
				}

				var argCount = Args.Where(x=> x == arg).ToArray().Count();
                if (argCount > 1) throw new ArgumentException("Wrong arg [" + arg + "] count! It's: "+ argCount);
			}

			var hasValidArgs = hasMask && hasFilePath && setAction;
			if (!hasValidArgs)
			{
				if(!setAction) throw new ArgumentException("You don't set your action!");

				if (!hasMask && !hasFilePath)
				{
					throw new ArgumentException("You don't append correct file path and mask for mp3.");
				}

				if (!hasFilePath)
				{
					throw new ArgumentException("You don't append correct file path.");
				}

				throw new ArgumentException("You don't append correct mask for mp3.");
			}

			for (var i = 0; i < Args.Length; i++)
			{
				if (Args[i] == "-path"		&& i + 1 < Args.Length && Args[i + 1] == "-mask"	||
				    Args[i] == "-mask"		&& i + 1 < Args.Length && Args[i + 1] == "-path" ||
					Args[i] == "-path"		&& i + 1 < Args.Length && Args[i + 1] == "-action"	||
					Args[i] == "-action"	&& i + 1 < Args.Length && Args[i + 1] == "-path" ||
					Args[i] == "-action"	&& i + 1 < Args.Length && Args[i + 1] == "-mask"	||
					Args[i] == "-mask"		&& i + 1 < Args.Length && Args[i + 1] == "-action"	  )
				{
					throw new ArgumentException("You don't append correct file path and mask for mp3.");
				}

				if ( (Args[i] == "-path" || Args[i] == "-mask" || Args[i] == "-action") && (i + 1 >= Args.Length) )
				{
					throw new ArgumentException("You don't append correct file path and mask for mp3.");
				}
			}


			return true;
		}

		/// <summary>
		/// Extracting args
		/// </summary>
		/// <returns>Dictionary with data extracted from args</returns>
		public Args ExtactArgs()
		{
			var key = new Queue<string>();
			var data = new Dictionary<string, string>();
			foreach (var arg in Args)
			{
				if (key.Count == 0)
				{
					key.Enqueue(arg);
					continue;
				}

				data.Add(key.Dequeue(), arg);
			}

			ProgramAction action;

			switch (data["-action"])
			{
				case "analyse":
					action = ProgramAction.Analyse;
					break;
				case "file-rename":
					action = ProgramAction.FileRename;
					break;
				case "change-tags":
					action = ProgramAction.Mp3Edit;
					break;
				default:
					throw new ArgumentException("action can be [analyse|file-rename|change-tags]");
			}

			return new Args(data["-path"], data["-mask"], action);
		}

	}
}