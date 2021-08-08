using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoClient;
using System.Threading.Tasks;

namespace ClientTests
{
    [TestClass]
    public class IntegrationTests
    {
        [TestMethod]
        public async Task ToDoTaskExistsAfterAdd()
        {
            var logger = new FakeToDoLogger();
            var reader = new FakeToDoReader(new List<string> { "add", "test task", "false", "list", "q"});
            var client = new WrappedHttpClient();
            var menuInputHandler = new MenuInputHandler(logger, reader, client);

            await menuInputHandler.HandleUsersInput();
            var taskId=int.Parse(logger.Messages[3].Split(' ').Last());
            var actualTask = GetTaskFromToDoList(logger.Messages[5], taskId);

            Assert.AreEqual($"{taskId}. test task  [ ]", actualTask);
        }

        private string GetTaskFromToDoList(string toDoList, int taskId)
        {
            var startIndex = toDoList.IndexOf(taskId.ToString());
            var length = toDoList.Substring(startIndex).IndexOf("\r\n");
            return toDoList.Substring(startIndex, length);
        }
    }
}
