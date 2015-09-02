using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using mp3lib;
using mp3lib.Args_Managing;
using mp3lib.Communication_With_User;
using mp3lib.Core;
using mp3lib.Core.Actions;
using mp3lib.Rollback;

namespace ConsoleMp3TagEditor
{
	public static class Program
	{
		private static readonly string RollbackData = Environment.CurrentDirectory + "/restoreData.json";

		public static void Main(string[] args)
		{
			/*try
			{*/
			var argsManager = new ArgsManager(args);
			argsManager.CheckArgsValidity();

			var data = argsManager.ExtactArgs();
			Mp3File mp3;
			IEnumerable<IMp3File> mp3Files;
			switch (data.Action)
			{
				case ProgramAction.Analyse:
					mp3Files = Directory.GetFiles(data.Path, "*.mp3").Select(file => new Mp3File(file)).ToArray();
					var pathAnalyser = new Mp3FileAnalyser(mp3Files, data.Mask);

					var differences = pathAnalyser.GetDifferences();
					new CommunicationWithUser().ShowDifferences(differences, new ConsoleCommunication());

					foreach (var mp3File in mp3Files)
					{
						mp3File?.Dispose();
					}

					break;

				case ProgramAction.Mp3Edit:
					using (mp3 = new Mp3File(data.Path))
					{
						var tagsChanger = new Mp3TagChanger(mp3, data.Mask, new FileSaver(RollbackData));
						tagsChanger.ChangeTags();
					}
					break;

				case ProgramAction.FileRename:
					using (mp3 = new Mp3File(data.Path))
					{
						var renamer = new Mp3FileNameChanger(mp3, data.Mask, new FileSaver(RollbackData));
						var newFileName = renamer.GetNewFileName();
						mp3.ChangeFileName(newFileName);
					}
					break;

				case ProgramAction.Sync:
					mp3Files = Directory.GetFiles(data.Path, "*.mp3").Select(file => new Mp3File(file)).ToArray();
					var syncer = new Mp3Syncing(mp3Files, data.Mask, new ConsoleCommunication(), new FileSaver(RollbackData));
					syncer.SyncFiles();

					foreach (var mp3File in mp3Files)
					{
						mp3File?.Dispose();
					}

					break;

				case ProgramAction.Rollback:

					using (var rollbackSystem = new RollbackManager(new FileSaver(RollbackData)))
					{
						var state = rollbackSystem.Rollback();

						Console.WriteLine(
							state.State == RollbackState.Fail
								? $"Error occured while trying doing rollback. Details : \n\tError:{state.FailReason}"
								: $"Rollback success.");
					}

					break;

				default:
					throw new ArgumentOutOfRangeException();
			}

			Console.WriteLine("Done!");
			/*}
			catch (Exception e)
			{
#if DEBUG
				Console.WriteLine("Exeption: \r\n{0} \r\n\nAt:\n{1}", e.Message, e.StackTrace);
#else
				Console.WriteLine("Error occured: {0}", e.Message);
#endif
			}
			finally
			{
				Console.ReadKey();
			}*/
		}
	}
}
