using System;
using System.Collections.Generic;

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
			if (Args.Length != 4)
			{
				throw new ArgumentException("Expected usage: {0} -file \"[path to file]\" -mask \"[mask for changing title]\"");
			}

			var hasFilePath = false;
			var hasMask = false;
			foreach (var arg in Args)
			{
				switch (arg)
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
					throw new ArgumentException("You don't append correct file path and mask for mp3.");
				}
				if (!hasFilePath)
				{
					throw new ArgumentException("You don't append correct file path.");
				}
				throw new ArgumentException("You don't append correct mask for mp3.");
			}

			for (int i = 0; i < Args.Length; i++)
			{
				if (Args[i] == "-file" && i + 1 < Args.Length && Args[i + 1] == "-mask" ||
				    Args[i] == "-mask" && i + 1 < Args.Length && Args[i + 1] == "-file")
				{
					throw new ArgumentException("You don't append correct file path and mask for mp3.");
				}

				if ( (Args[i] == "-file" || Args[i] == "-mask") && (i + 1 >= Args.Length) )
				{
					throw new ArgumentException("You don't append correct file path and mask for mp3.");
				}
			}


			return true;
		}

		/// <summary>
		/// Extracting args
		/// </summary>
		/// <param name="args">Array with args</param>
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

			return new Args(data["-file"], data["-mask"]);
		} 

	}
}