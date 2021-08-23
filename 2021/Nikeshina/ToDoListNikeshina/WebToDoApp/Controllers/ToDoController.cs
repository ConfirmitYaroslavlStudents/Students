using ToDoListNikeshina;
using ToDoListNikeshina.Validators;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebToDoApp.Controllers
{
    [ApiController]
    [Route("api/todo-items")]
    public class ToDoController : ControllerBase
    {
        private ToDoList _list;
        private FileManager _fileManager = new FileManager();

        public ToDoController( FileManager fileManager, ToDoList list)
        {
            _fileManager = fileManager;
            _list = list;
        }

        [HttpGet]
        public List<Task> GetToDoList() => _list.GetListOfTasks();

        [HttpPost]
        public ActionResult AddToDoItem([FromBody] string description)
        {
            var validator = new CheckLengthDescription(true, description);
            if (!validator.Validate())
                return BadRequest();

            _list.Add(description, StatusOfTask.Todo);
            _fileManager.Save(_list.GetListOfTasks());
            return NoContent(); ;
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteToDoItem(int id)
        {
            var validator = new ValidatorTaskNumber(true, _list, id);
            if (!validator.Validate())
                return NotFound();

            _list.Delete(id);
            _fileManager.Save(_list.GetListOfTasks());
            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult ChangeToDoItem(int id, [FromBody] Task item)
        {
            var validator = new ValidatorTaskNumber(true, _list, id);
            if (!validator.Validate())
                return NotFound();

            var validatorCounttaskInProgress = new ValidatorCheckTaskCountInProgress(true, _list, validator, item.Status);
            if (!validatorCounttaskInProgress.Validate())
                return BadRequest();


            if (item.Name == null)
            {
                _list.ChangeStatus(id, item.Status);
            }
            else
            {
                var descriptionValidator = new CheckLengthDescription(true, item.Name);
                if (!descriptionValidator.Validate())
                    return BadRequest();

                _list.ChangeStatus(id, item.Status);
                _list.Edit(id, item.Name);
            }

            _fileManager.Save(_list.GetListOfTasks());
            return NoContent();
        }
    }
}
