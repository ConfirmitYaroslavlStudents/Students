using Microsoft.AspNetCore.Mvc;
using ToDo;
using System.Collections.Generic;

namespace ToDoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly ILoaderAndSaver _loaderAndSaver;
        private readonly ToDoList _toDoList;

        public ToDoController(ILoaderAndSaver loaderSaver)
        {
            _loaderAndSaver = loaderSaver;
            _toDoList = _loaderAndSaver.Load();
        }

        [HttpGet]
        public string GetToDoList()
        {
            return _toDoList.ToString();
        }

        [HttpPost]
        public void PostTask([FromBody] Task task)
        {
            _toDoList.Add(task);
            _loaderAndSaver.Save(_toDoList);
        }

        [HttpDelete ("{taskNumber}")]
        public void DeleteTask(int taskNumber)
        {
            _toDoList.Remove(taskNumber);
            _loaderAndSaver.Save(_toDoList);
        }

        [HttpPatch("{taskNumber}")]
        public void PatchTask(int taskNumber, [FromBody] Task task)
        {
            var oldTask = _toDoList[taskNumber];
            if (task.Text != null)
                oldTask.Text = task.Text;
            if (task.IsDone != null)
                oldTask.IsDone = task.IsDone;
            _loaderAndSaver.Save(_toDoList);
        }
    }
}
