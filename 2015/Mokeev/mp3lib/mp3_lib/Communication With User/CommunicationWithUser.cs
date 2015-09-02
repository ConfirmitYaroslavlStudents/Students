using System;
using System.Collections.Generic;
using mp3lib.Args_Managing;
using mp3lib.Core;

namespace mp3lib.Communication_With_User
{
	public class CommunicationWithUser
	{


		//TODO: move to separate class {READY}
		public void ShowDifferences(IEnumerable<FileDifferences> differences, IRequestable requestable)
		{
			foreach (var file in differences)
			{
				requestable.SendMessage($"This file has differences: {file.Mp3File.FilePath}");
				foreach (var diff in file.Diffs)
				{
					requestable.SendMessage($"{diff.Key} in Mp3File Name: {diff.Value.FileNameValue}");
					requestable.SendMessage($"{diff.Key} in Tags: {diff.Value.TagValue}");
					requestable.SendMessage("\n");
				}
			}
		}

		//TODO: move to separate class {READY}
		public UserData GetInfoFromUser(TagType tag, Diff diff, IMp3File file, ICommunication communication)
		{
			communication.SendMessage($"File: {file.FilePath}");
			communication.SendMessage($"There is a problem with tag \"{tag}\". ");
			communication.SendMessage(
				$"You can enter tag from: \n\t1) File name (Data: \"{diff.FileNameValue}\"), \n\t2) Mp3 Tags (Data: \"{diff.TagValue}\"), \n\t3) Manual");

			while (true)
			{
				communication.SendMessage("Your choise (number): ");
				SyncActions inputData;
				var choiseCorrect = Enum.TryParse(communication.GetResponse(), out inputData);
				if (!choiseCorrect)
				{
					communication.SendMessage("Wrong input!");
					communication.SendMessage("You sholud enter number with action!");
					continue;
				}

				switch (inputData)
				{
					case SyncActions.FromFileName:
						return new UserData(inputData, diff.FileNameValue);
					case SyncActions.FromTags:
						return new UserData(inputData, diff.TagValue);
					case SyncActions.Manual:
						communication.SendMessage("Enter text for tag \"" + tag + "\"");
						return new UserData(inputData, communication.GetResponse());
				}
			}
		}
	}
}