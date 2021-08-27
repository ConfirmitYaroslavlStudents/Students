using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TodoWeb.Requests;
using Task = ToDoLibrary.Task;

namespace TodoWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TodoController(IMediator mediator)
            => _mediator = mediator;

        [HttpGet]
        public IEnumerable<Task> ShowTasks()
        {
            var getRequest = new GetRequest();
            _mediator.Send(getRequest);

            return getRequest.Tasks;
        }

        [HttpPost]
        public ActionResult<string> AddNewTask(string text, string status)
        {
            if (text == null)
                return StatusCode(400, "");

            var postRequest = new PostRequest() { Text = text, Status = status };
            _mediator.Send(postRequest);

            return StatusCode(postRequest.HandlingResult.Item1, postRequest.HandlingResult.Item2);
        }

        [HttpPatch("{taskId}")]
        public ActionResult<string> EditTask(string text, string status, long taskId)
        {
            var patchRequest = new PatchRequest() { TaskId = taskId, Text = text, Status = status };
            _mediator.Send(patchRequest);

            return StatusCode(patchRequest.HandlingResult.Item1, patchRequest.HandlingResult.Item2);
        }

        [HttpDelete("{taskId}")]
        public ActionResult<string> DeleteTask(long taskId)
        {
            var deleteRequest = new DeleteRequest() { TaskId = taskId };
            _mediator.Send(deleteRequest);

            return StatusCode(deleteRequest.HandlingResult.Item1, deleteRequest.HandlingResult.Item2);
        }
    }
}
