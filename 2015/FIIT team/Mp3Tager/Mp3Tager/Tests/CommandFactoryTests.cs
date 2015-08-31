using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommandCreation;
using Tests.Fakes;

namespace Tests
{
    [TestClass]
    public class CommandFactoryTests
    {
        [ExpectedException(typeof(InvalidOperationException), "Invalid operation: there is no such command!")]
        [TestMethod]
        public void CommandFactory_NoSuchCommand_ThrowException()
        {
            new CommandFactory().ChooseCommand("some command", new FakeMp3File(new FileLib.Mp3Tags(), ""), new FakeWorker(), "");
        }

        [TestMethod]
        public void CommandFactory_ChooseRenameCommand()
        {
            var actualCommand = new CommandFactory().ChooseCommand("rename", new FakeMp3File(new FileLib.Mp3Tags(), ""), new FakeWorker(), "");
            Assert.IsInstanceOfType(actualCommand, typeof(RenameCommand));
        }

        [TestMethod]
        public void CommandFactory_ChooseChangeTagsCommand()
        {
            var actualCommand = new CommandFactory().ChooseCommand("changetags", new FakeMp3File(new FileLib.Mp3Tags(), ""), new FakeWorker(), "");
            Assert.IsInstanceOfType(actualCommand, typeof(ChangeTagsCommand));
        }

        [TestMethod]
        public void CommandFactory_ChooseAnalyseCommand()
        {
            var actualCommand = new CommandFactory().ChooseCommand("analyse", new FakeMp3File(new FileLib.Mp3Tags(), ""), new FakeWorker(), "");
            Assert.IsInstanceOfType(actualCommand, typeof(AnalyseCommand));
        }

        [TestMethod]
        public void CommandFactory_ChooseSyncCommand()
        {
            var actualCommand = new CommandFactory().ChooseCommand("sync", new FakeMp3File(new FileLib.Mp3Tags(), ""), new FakeWorker(), "");
            Assert.IsInstanceOfType(actualCommand, typeof(SyncCommand));
        }
    }
}
