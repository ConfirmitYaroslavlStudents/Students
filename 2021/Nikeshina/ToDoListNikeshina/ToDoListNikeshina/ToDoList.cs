using System.Collections.Generic;

namespace ToDoListNikeshina
{
    public class ToDoList
    {
        internal List<Task> _list = new List<Task>();
        private int _countOfId;
        private List<int> _listOfId;

        public ToDoList(List<Task> inputList, int idCount)
        {
            _list = inputList;
            _countOfId = idCount;
            _listOfId = new List<int>();
        }

        public int Count() => _list.Count;

        
        public void Add(string description, StatusOfTask status, int id)
        {
            var item = new Task(description, status, id);
            _list.Add(item);
            _listOfId.Add(id);
        }

        public void Add(string description, StatusOfTask status)
        {
            _countOfId++;
            var item = new Task(description,status, _countOfId);
            _list.Add(item);
            _listOfId.Add(_countOfId);
        }

        public void Delete(int id)
        {
            _list.RemoveAt(id);
            _listOfId.Remove(id);
        }

        public void ChangeStatus(int id)
        {
            _list[id].ChangeStatus();
        }

        public void ChangeStatus(int id, StatusOfTask status)
        {
            this[id].ChangeStatus(status);
        }

        public void Edit(int id, string description)
        {
            this[id].ChangeName(description);
        }

        internal ToDoList CopyList()
        {
            int maxId = 0;
            var newItem = new List<Task>();
            foreach (var task in _list)
            {
                var newtask = new Task(task.Name, task.Status, task._id);
                newItem.Add(newtask);
                if (task._id > maxId)
                    maxId = task._id;
            }

            return new ToDoList(newItem,maxId);
        }
        public Task this[int id]
        {
            get 
            {
                return FindTaskWithId(id);
            }
        }

        public int GetCountTasksInProgress()
        {
            var count = 0;

            foreach (var task in _list)
            {
                if (task.Status == StatusOfTask.InProgress)
                    count++;
            }

            return count;
        }

        public List<Task> GetListOfTasks() => _list;

        private Task FindTaskWithId(int id)
        {
            foreach (var task in _list)
                if (task._id == id)
                    return task;

            return null;
        }
    }
}
