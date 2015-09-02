using System;
using mp3lib;
using mp3lib.Communication_With_User;

namespace ConsoleMp3TagEditor
{
	public class ConsoleCommunication : ICommunication
	{
		public void SendMessage(string str)
		{
			Console.WriteLine(str);
		}

		public string GetResponse()
		{
			return Console.ReadLine();
		}
	}
}