using System.Collections.Generic;
using ToDoListLib;

namespace ToDoListApp
{
    public class ToDoListApp
    {
        private ToDoList _toDoList;

        public ToDoListApp(List<Task> toDoList)
        {
            _toDoList = new ToDoList(toDoList);
        }
        public ToDoListApp()
        {
            _toDoList = new ToDoList();
        }

        public void AddTask(string name = "NewTask", string description = "", TaskStatus status = TaskStatus.NotDone)
        {
            _toDoList.AddTask(name, description, status);
        }
        public Task GetTask(string name)
        {
            return _toDoList.GetTask(name);
        }
        public List<Task> GetAllTask()
        {
            return _toDoList.GetAllTask();
        }

    }
}
