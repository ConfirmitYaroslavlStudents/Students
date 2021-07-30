using Microsoft.AspNetCore.Mvc;
using ToDo;

namespace ToDoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly ILoaderAndSaver _loaderAndSaver;
        private readonly ToDo.ILogger _logger;
        private readonly ToDoList _toDoList;

        public ToDoController(ILoaderAndSaver loaderSaver, ToDo.ILogger logger)
        {
            _loaderAndSaver = loaderSaver;
            _logger = logger;
            _toDoList = _loaderAndSaver.Load();
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
            _loaderAndSaver.Save(_toDoList);
        }

        [HttpDelete ("{taskNumber}")]
        public void DeleteTask(int taskNumber)
        {
            _toDoList.RemoveAt(taskNumber - 1);
            _logger.Log("Задание удалено");
            _loaderAndSaver.Save(_toDoList);
        }

        [HttpPatch("{taskNumber}")]
        public void PatchTaskText(int taskNumber, [FromBody] string taskText)
        {
            _toDoList[taskNumber - 1].Text = taskText;
            _logger.Log("Текст задания обновлен");
            _loaderAndSaver.Save(_toDoList);
        }

        [HttpPatch("{taskNumber}")]
        public void PatchTaskStatus(int taskNumber, [FromBody] bool taskStatus)
        {
            var task = _toDoList[taskNumber - 1];
            task.IsDone = taskStatus;
            _logger.Log("Статус задания обновлен");
            _loaderAndSaver.Save(_toDoList);
        }
    }
}
