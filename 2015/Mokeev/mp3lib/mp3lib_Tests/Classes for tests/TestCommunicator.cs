using System.Collections.Generic;
using mp3lib;

namespace mp3lib_Tests.Classes_for_tests
{
	public class TestCommunicator : ICommunication
	{
		public Queue<string> Out { get; set; }
		public Queue<string> In { get; set; }


		public TestCommunicator() : this (new Queue<string>(), new Queue<string>())
		{
		}

		public TestCommunicator(Queue<string> @out, Queue<string> @in)
		{
			Out = @out;
			In = @in;
		}

		public void SendMessage(string str)
		{
			Out.Enqueue(str);
		}

		public string GetResponse()
		{
			return In.Dequeue();
		}
	}
}