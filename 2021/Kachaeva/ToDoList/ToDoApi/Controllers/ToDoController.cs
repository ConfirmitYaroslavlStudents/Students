using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ToDo;

namespace ToDoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly ILogger<ToDoController> _logger;
        private readonly IToDoListLoaderSaver _loaderSaver;
        private readonly ToDoList _toDoList;

        public ToDoController(/*IToDoListLoaderSaver loaderSaver*/ ILogger<ToDoController> logger)
        {
            //_loaderSaver = loaderSaver;
            _logger = logger;
            _loaderSaver=new FileLoaderSaver("ToDoList.txt");
            _toDoList = _loaderSaver.Load();
        }

        [HttpGet]
        public IEnumerable<string> GetToDoList()
        {
            if (_toDoList.Count == 0)
                _logger.LogInformation("Список пуст");
            foreach (var task in _toDoList)
                yield return task.ToString();
        }

        [HttpPost]
        public void PostTask([FromBody] string taskText)
        {
            _toDoList.Add(new ToDo.Task(taskText));
            _logger.LogInformation("Задание добавлено");
        }

        [HttpDelete]
        public void DeleteTask(int taskNumber)
        {
            _toDoList.Remove(taskNumber - 1);
            _logger.LogInformation("Задание удалено");
        }

        [HttpPut]
        public void PutTaskText(int taskNumber, [FromBody] string taskText)
        {
            _toDoList[taskNumber - 1].Text = taskText;
            _logger.LogInformation("Текст задания изменен");
        }

        [HttpPut]
        public void PutTaskStatus(int taskNumber)
        {
            var task = _toDoList[taskNumber - 1];
            task.IsDone = !task.IsDone;
            _logger.LogInformation("Статус задания изменен");
        }
    }
}
