using System;
using System.Collections.Generic;
using System.Linq;
using ToDoListNikeshina.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToDoListNikeshina;

namespace WebToDo.Controllers
{
    [ApiController]
    [Route("api/todo-items")]
    public class ToDoController : ControllerBase
    {
        private ToDoList _list;
        private FileManager _fileManager = new FileManager();

        private readonly ILogger<ToDoController> _logger;

        public ToDoController(ILogger<ToDoController> logger)
        {
            _logger = logger;
            _list = new ToDoList(_fileManager.Load());
        }

        [HttpGet]
        public List<ToDoListNikeshina.Task> Get()
        {
            //return _list.GetListOfTask();
            return new List<Task>();
        }

        [HttpPost]
        public ActionResult<string> Post([FromBody] string description)
        {
            if (description.Length == 0 && description.Length > 50)
                return Messages.incorrectDescriptionLength;

            _list.Add(new ToDoListNikeshina.Task(description, StatusOfTask.Todo));
            _fileManager.Save(_list.GetListOfTask());
            return Messages.completed;
        }

        [HttpDelete("index}")]
        public ActionResult<string> Delete(int index)
        {
            if (index < 0 && index > _list.Count())
                return Messages.incorrectTaskNumber;

            _list.Delete(index);
            _fileManager.Save(_list.GetListOfTask());
            return Messages.completed;
        }

    }
}
