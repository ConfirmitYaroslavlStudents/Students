using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;
using ToDoApiDependencies;
using Microsoft.AspNetCore.Cors;

namespace ToDoApi.Controllers
{
    [ApiController]
    [Route("todo")]
    
    [EnableCors("MyPolicy")]
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
        public IEnumerable<ToDoTask> GetToDoList()
        {
            return _toDoList.Select(toDoTask => toDoTask.Value);
        }

        [HttpGet("{taskId}")]
        public ActionResult<ToDoTask> GetToDoTask(int taskId)
        {
            if (!_toDoList.ContainsKey(taskId))
                return NotFound();
            return _toDoList[taskId];
        }

        [HttpPost]
        public IActionResult PostTask([FromBody] ToDoTask toDoTask)
        {
            _toDoList.Add(toDoTask);
            _loaderAndSaver.Save(_toDoList);
            return CreatedAtAction(nameof(GetToDoTask), new {taskId = toDoTask.Id}, toDoTask);
        }

        [HttpDelete ("{taskId}")]
        public IActionResult DeleteTask(int taskId)
        {
            if (!_toDoList.ContainsKey(taskId))
                return NotFound();
            _toDoList.Remove(taskId);
            _loaderAndSaver.Save(_toDoList);
            return NoContent();
        }

        [HttpPatch("{taskId}")]
        public IActionResult PatchTask(int taskId, [FromBody] PatchToDoTask toDoTask)
        {
            if (!_toDoList.ContainsKey(taskId))
                return NotFound();
            var oldToDoTask = _toDoList[taskId];
            if (toDoTask.IsPatchContainsText)
                oldToDoTask.Text = toDoTask.Text;
            if (toDoTask.IsPatchContainsIsDone)
                oldToDoTask.IsDone = toDoTask.IsDone;
            if (toDoTask.IsPatchContainsTags)
                oldToDoTask.Tags = toDoTask.Tags;
            _loaderAndSaver.Save(_toDoList);
            return NoContent();
        }
    }
}
