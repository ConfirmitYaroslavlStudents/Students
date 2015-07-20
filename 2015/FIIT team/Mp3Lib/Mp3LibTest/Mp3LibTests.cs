using System;
using Moq;
using NUnit.Framework;

namespace Mp3LibTest
{
    [TestFixture]
    public class Mp3LibTests
    {
        [Test]
        [TestCase(1, new[] { "help" }, TestName = "Test1")]
        [TestCase(2, new[] { "rename", @"D:\Music\aaa.mp3", "{track}_{artist}_{title}" }, TestName = "Test2")]
        [TestCase(3, new[] { "changeTag", @"D:\Music\aaa.mp3", "artist", "Shakira" }, TestName = "Test3")]
        public void CheckCommandExecution(int val, string[] args)
        {
            Mock<Mp3Lib.Mp3Lib> mp3LibMock = new Mock<Mp3Lib.Mp3Lib>((object)args);
            mp3LibMock.Setup(x => x.CheckArgs(args[0])).Returns(true);
            mp3LibMock.Object.ExecuteCommand();
            
            mp3LibMock.Verify(x => x.ShowHelp(), Times.Exactly(Convert.ToInt32(args[0] == "help")));
            mp3LibMock.Verify(x => x.Rename(args), Times.Exactly(Convert.ToInt32(args[0] == "rename")));
            mp3LibMock.Verify(x => x.ChangeTag(args), Times.Exactly(Convert.ToInt32(args[0] == "changeTag")));
        }

        [Test]
        public void ExecuteCommand_NoCommandExecuted()
        {
            string[] args = {"rename"};
            Mock<Mp3Lib.Mp3Lib> mp3LibMock = new Mock<Mp3Lib.Mp3Lib>((object)args);
            mp3LibMock.Setup(x => x.CheckArgs(args[0])).Returns(false);
            mp3LibMock.Object.ExecuteCommand();

            mp3LibMock.Verify(x => x.ShowHelp(), Times.Never);
            mp3LibMock.Verify(x => x.Rename(args), Times.Never);
            mp3LibMock.Verify(x => x.ChangeTag(args), Times.Never);
        }
    }
}
