using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoLibrary;
using ToDoLibrary.Storages;
using ToDoLibrary.CommandHandler;
using ToDoLibrary.Commands;

namespace TestProjectForToDoLibrary
{
    [TestClass]
    public class TestsForCommandFromWebHandler
    {
        private TasksStorage _storage = new TasksStorage();

        private void RunHandle(ICommand command)
        {
            var consoleCommandHandler = new WebCommandHandler(command);
            consoleCommandHandler.SetStorage(_storage);
            consoleCommandHandler.Run();
        }

        [TestMethod]
        public void CorrectHandleAddCommand()
        {
            RunHandle(new AddCommand{NewTask = new Task{Text = "world"}});

            var task = _storage.Get();

            Assert.AreEqual(1, task.Count);
            Assert.AreEqual("[1] world", task[0].ToString());
        }

        [TestMethod]
        public void CorrectHandleDeleteCommand()
        {
            var task = new List<Task>
                    {
                        new Task{Text = "world", TaskId =  1},
                        new Task{Text = "war", TaskId = 2},
                        new Task {Text = "peace", TaskId = 3}
                    };
            _storage.Set(task);

            RunHandle(new DeleteCommand {TaskId  =1});
            RunHandle(new DeleteCommand {TaskId  =3});

            task = _storage.Get();

            Assert.AreEqual(1, task.Count);
            Assert.AreEqual("[2] war", task[0].ToString());
        }

        [TestMethod]
        public void SuccessfullyHandlePatchRequest()
        {
            var tasks = new List<Task>
            {
                new Task{Text = "IFirst",TaskId = 1},
                new Task{Text = "ISecond",TaskId = 2, Status = StatusTask.IsProgress},
                new Task {Text = "IThird",TaskId = 3}
            }; 
            _storage.Set(tasks);

            RunHandle(new EditTaskCommand
            {
                EditTextCommand = new EditTextCommand { TaskId = 2, Text = "Hey!" },
                ToggleStatusCommand = new ToggleStatusCommand { TaskId = 2, Status = StatusTask.Done }
            });

             tasks = _storage.Get();

            Assert.AreEqual("[2] Hey! ●", tasks[1].ToString());
        }
    }
}