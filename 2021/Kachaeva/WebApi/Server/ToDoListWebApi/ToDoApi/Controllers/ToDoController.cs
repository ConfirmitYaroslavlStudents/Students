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
        public void PostTask([FromBody] string taskText)
        {
            _toDoList.Add(new Task(taskText));
            _loaderAndSaver.Save(_toDoList);
        }

        [HttpDelete ("{taskNumber}")]
        public void DeleteTask(int taskNumber)
        {
            _toDoList.Remove(taskNumber);
            _loaderAndSaver.Save(_toDoList);
        }

        [HttpPatch("{taskNumber}")]
        public void PatchTaskText(int taskNumber, [FromBody] Task task)
        {
            //var oldTask = _toDoList[taskNumber - 1];
            //if (task.Text != null && task.Text != oldTask.Text) 
            //{
            //    oldTask.Text = task.Text;
            //    _logger.Log("Текст задания обновлен");
            //}

            //if (task.IsDone != oldTask.IsDone)
            //{
            //    oldTask.IsDone
            //}

            //_loaderAndSaver.Save(_toDoList);
        }
    }
}
