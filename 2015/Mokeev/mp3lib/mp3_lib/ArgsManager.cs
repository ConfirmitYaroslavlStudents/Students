using System;
using System.Collections.Generic;
using System.Linq;

namespace mp3lib
{
	public class ArgsManager
	{
		//=========IMPORTANT CONSTANTS===========
		private const string PATH				= "-path";
		private const string MASK				= "-mask";
		private const string ACTION				= "-action";
		//--------------------------------------
		private const string ActionAnalyse		= "analyse";
		private const string ActionFileRename	= "file-rename";
		private const string ActionChangeTags	= "change-tags";
		private const string ActionSync			= "sync";
		//======================================



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
				throw new ArgumentException("Expected usage: \n\t" +
												"-action \t[analyse|file-rename|change-tags|sync] \n\t" +
												"-path  \t\t\"[path to file]\" \n\t" +
												"-mask  \t\t\"[mask for changing title]\""
											);
			}

			var hasFilePath = false;
			var hasMask = false;
			var setAction = false;
			foreach (var arg in Args)
			{
				switch (arg)
				{
					case PATH:
						hasFilePath = true;
						break;
					case MASK:
						hasMask = true;
						break;
					case ACTION:
						setAction = true;
						break;
				}

				var argCount = Args.Where(x => x == arg).ToArray().Count();
				if (argCount > 1) throw new ArgumentException("Wrong arg [" + arg + "] count! It's: " + argCount);
			}

			var hasValidArgs = hasMask && hasFilePath && setAction;
			if (!hasValidArgs)
			{
				if (!setAction) throw new ArgumentException("You don't set your action!");

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
                // TODO: is it readable at all? {READY}
				if 
				(
					i + 1 < Args.Length		&&
					(
						Args[i] == PATH		&& (Args[i + 1] == MASK || Args[i + 1] == ACTION) ||
						Args[i] == MASK		&& (Args[i + 1] == PATH	|| Args[i + 1] == ACTION) ||
						Args[i] == ACTION	&& (Args[i + 1] == PATH || Args[i + 1] == MASK)
					)
				)
				{
					throw new ArgumentException("You don't append correct file path and mask for mp3.");
				}

				if ((Args[i] == PATH || Args[i] == MASK || Args[i] == ACTION) && (i + 1 >= Args.Length))
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

			switch (data[ACTION])
			{
				case ActionAnalyse:
					action = ProgramAction.Analyse;
					break;
				case ActionFileRename:
					action = ProgramAction.FileRename;
					break;
				case ActionChangeTags:
					action = ProgramAction.Mp3Edit;
					break;
				case ActionSync:
					action = ProgramAction.Sync;
					break;
				default:
					throw new ArgumentException("-action can be [analyse|file-rename|change-tags]");
			}

			return new Args(data[PATH], data[MASK], action);
		}

	}
}