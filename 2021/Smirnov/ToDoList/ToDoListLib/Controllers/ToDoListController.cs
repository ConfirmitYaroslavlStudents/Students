using System.Collections.Generic;
using System.Linq;
using ToDoListLib.Models;
using Microsoft.AspNetCore.Mvc;
using ToDoListLib.SaveAndLoad;

namespace ToDoListLib.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListController
    {
        private readonly List<Task> _toDoList;
        private readonly ISaveAndLoad _saveAndLoad;

        public ToDoListController(ISaveAndLoad saveAndLoad)
        {
            _saveAndLoad = saveAndLoad;
            _toDoList = new List<Task>(_saveAndLoad.Load());
        }
        [HttpGet]
        public IEnumerable<Task> GetToDoList()
        {
            return _toDoList;
        }
        [HttpGet("{id}")]
        public Task GetTask(long id)
        {
            return FindTask(id);
        }
        [HttpPost]
        public void AddTask(Task task)
        {
            if (_toDoList.Count > 0)
                task.Id = _toDoList[^1].Id + 1;

            _toDoList.Add(task);

            _saveAndLoad.Save(_toDoList);
        }
        [HttpDelete("{id}")]
        public void DeleteTask(long id)
        {
            _toDoList.Remove(FindTask(id));

            _saveAndLoad.Save(_toDoList);
        }
        [HttpPut("{id}")]
        public void ChangeDescription(long id,[FromBody] string newDescription)
        {
            FindTask(id).Description = newDescription;

            _saveAndLoad.Save(_toDoList);
        }
        [HttpPut("{id}/{status}")]
        public void ChangeTaskStatus(long id, TaskStatus status)
        {
            FindTask(id).Status = status;

            _saveAndLoad.Save(_toDoList);
        }
        private Task FindTask(long id)
        {  
            return _toDoList.FirstOrDefault(x => x.Id == id);
        }
    }
}
