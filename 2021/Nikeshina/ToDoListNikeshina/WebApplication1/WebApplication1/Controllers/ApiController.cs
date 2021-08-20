using System.Collections.Generic;
using ToDoListNikeshina;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/todo-items")]
    public class ApiController : ControllerBase
    {
        private ToDoList _list;
        private FileManager _fileManager = new FileManager();

        private readonly ILogger<ApiController> _logger;

        public ApiController(ILogger<ApiController> logger)
        {
            _logger = logger;
            _list = new ToDoList(_fileManager.Load());
        }

        [HttpGet]
        public ActionResult<List<Task>> Get() => _list.GetListOfTasks();

        [HttpPost]
        public ActionResult Post([FromBody] string description)
        {
            if (IsDescriptionCorrect(description))
                return BadRequest();

            _list.Add(new Task(description, StatusOfTask.Todo));
            _fileManager.Save(_list.GetListOfTasks());
            return NoContent(); ;
        }

        [HttpDelete("{index}")]
        public ActionResult Delete(int index)
        {
            if (!IsIndexCorrect(index))
                return NotFound();

            _list.Delete(index);
            _fileManager.Save(_list.GetListOfTasks());
            return NoContent();
        }

        [HttpPatch("{index}")]
        public ActionResult Patch(int index, [FromBody] string command)
        {
            if (!IsIndexCorrect(index))
                return NotFound();

            var wordsOfCommand = command.Split(' ');
            if (wordsOfCommand[0] == "change")
                _list.ChangeStatus(index);
            else if (wordsOfCommand[0] == "edit")
            {
                var description = GetDescriptionFromCommandString(wordsOfCommand);
                if (!IsDescriptionCorrect(description))
                    return BadRequest();
                
                _list.Edit(index, description);
            }
            else
            {
                _logger.LogError(Messages.incorrectInputData);
                return BadRequest();
            }

            _fileManager.Save(_list.GetListOfTasks());
            return NoContent();
        }

        private string GetDescriptionFromCommandString(string[] words)
        {
            StringBuilder result = new StringBuilder();
            if (words.Length == 1)
                return result.ToString();

            for (int i = 1; i < words.Length - 1; i++)
            {
                result.Append(words[i] + " ");
            }
            result.Append(words[^1]);

            return result.ToString();
        }

        private bool IsIndexCorrect(int index)
        {
            if (index < 0 || index > _list.Count())
            {
                _logger.LogError(Messages.incorrectTaskNumber);
                return false;
            }

            return true;
        }

        private bool IsDescriptionCorrect(string description)
        {
            if(description.Length==0 || description.Length>50)
            {
                _logger.LogError(Messages.incorrectDescriptionLength);
                return false;
            }

            return true;
        }
    }
}
