using System;
using System.Collections.Generic;

namespace mp3lib
{
	public class ArgsManager
	{
		/// <summary>
		/// Validate args for mp3 file
		/// </summary>
		/// <param name="args">Array with args</param>
		/// <returns>true or throws an Exeptions</returns>
		/// <exception cref="ArgumentException">Wrong argument</exception>
		public void CheckArgsValidity(string[] args)
		{
			if (args.Length != 4)
			{
				throw new ArgumentException("Expected usage: {0} -file \"[path to file]\" -mask \"[mask for changing title]\"", "args");
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
					throw new ArgumentException("You don't append correct file path and mask for mp3.", "args");
				}
				if (!hasFilePath)
				{
					throw new ArgumentException("You don't append correct file path.", "args");
				}
				throw new ArgumentException("You don't append correct mask for mp3.", "args");
			}

			if (args[0] == "-file" && args[1] == "-mask" || args[0] == "-mask" && args[1] == "-file")
			{
				throw new ArgumentException("You don't append correct file path and mask for mp3.", "args");
			}

			return;
		}

		/// <summary>
		/// Extracting args
		/// </summary>
		/// <param name="args">Array with args</param>
		/// <returns>Dictionary with data extracted from args</returns>
		public Args ExtactArgs(IEnumerable<string> args)
		{
			var key = new Queue<string>();
			var data = new Dictionary<string, string>();
			foreach (var arg in args)
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