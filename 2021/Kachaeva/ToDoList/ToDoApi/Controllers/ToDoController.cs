using Microsoft.AspNetCore.Mvc;
using ToDo;

namespace ToDoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly ILoaderSaver _loaderSaver;
        private readonly ToDo.ILogger _logger;
        private readonly ToDoList _toDoList;

        public ToDoController(ILoaderSaver loaderSaver, ToDo.ILogger logger)
        {
            _loaderSaver = loaderSaver;
            _logger = logger;
            _toDoList = _loaderSaver.Load();
        }

        [HttpGet]
        public string GetToDoList()
        {
            if (_toDoList.Count == 0)
                _logger.Log("Список пуст");
            return _toDoList.ToString();
        }

        [HttpPost]
        public void PostTask([FromBody] string taskText)
        {
            _toDoList.Add(new ToDo.Task(taskText));
            _logger.Log("Задание добавлено");
            _loaderSaver.Save(_toDoList);
        }

        [HttpDelete ("{taskNumber}")]
        public void DeleteTask(int taskNumber)
        {
            _toDoList.RemoveAt(taskNumber - 1);
            _logger.Log("Задание удалено");
            _loaderSaver.Save(_toDoList);
        }

        [HttpPut]
        public void PutTaskText([FromBody] PutTaskTextRequest taskRequest)
        {
            _toDoList[taskRequest.TaskNumber - 1].Text = taskRequest.TaskText;
            _logger.Log("Текст задания изменен");
            _loaderSaver.Save(_toDoList);
        }

        [HttpPut("{taskNumber}")]
        public void PutTaskStatus(int taskNumber)
        {
            var task = _toDoList[taskNumber - 1];
            task.IsDone = !task.IsDone;
            _logger.Log("Статус задания изменен");
            _loaderSaver.Save(_toDoList);
        }
    }
}
