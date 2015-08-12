using System.Collections.Generic;
using mp3lib;

namespace mp3lib_Tests.Classes_for_tests
{
	public class TestCommunicator : ICommunication
	{
		public Queue<string> UserInput { get; private set; }
		public List<string> ProgramOutput{ get; private set; }

		public TestCommunicator(IEnumerable<string> userInput)
		{
			UserInput = new Queue<string>();
			ProgramOutput = new List<string>();

			foreach (var str in userInput)
			{
				UserInput.Enqueue(str);
			}
		}

		public void SendMessage(string str)
		{
			ProgramOutput.Add(str);
		}

		public string GetResponse()
		{
			return UserInput.Dequeue();
		}
	}
}