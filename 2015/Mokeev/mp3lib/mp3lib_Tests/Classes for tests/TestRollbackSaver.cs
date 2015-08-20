using System.Collections.Generic;
using mp3lib.Rollback;

namespace mp3lib_Tests.Classes_for_tests
{
	public class TestRollbackSaver : TestCommunicator, ISaver
	{
		public TestRollbackSaver(IEnumerable<string> userInput) : base(userInput)
		{
		}
	}
}