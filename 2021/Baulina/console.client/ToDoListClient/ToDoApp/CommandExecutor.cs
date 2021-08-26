using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using InputOutputManagers;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using ToDoApp.CustomClient;
using ToDoApp.Models;

namespace ToDoApp
{
    public class CommandExecutor : IExecute
    {
        private readonly IConsoleExtended _console;
        private readonly Client _client;
        public bool IsWorking { get; private set; }

        public CommandExecutor(IConsoleExtended console, Client client)
        {
            _console = console;
            _client = client;
            IsWorking = true;
        }

        public Func<Task> GetCommand(string operationName)
        {
            return operationName switch
            {
                "add" => Add,
                "edit" => Edit,
                "complete" => Complete,
                "incomplete" => Incomplete,
                "delete" => Delete,
                "list" => List,
                "exit" => Exit,
                _ => Error
            };
        }

        public async Task ProcessOperation(string operationName)
        {
            var command = GetCommand(operationName);
            try
            {
                await command();
            }
            catch (ArgumentOutOfRangeException)
            {
                _console.PrintIncorrectNumberWarning();
            }
            catch (InvalidOperationException)
            {
                _console.PrintIncorrectNumberWarning();
            }
            catch (Exception)
            {
                _console.PrintErrorMessage();
            }
        }

        public async Task<IEnumerable<ToDoItem>> GetActualToDoList()
        {
            var response = await Call(() => _client.Get());
            var responseString = await response.Content.ReadAsStringAsync();
            return (JsonConvert.DeserializeObject<IEnumerable<ToDoItem>>(responseString) ?? Array.Empty<ToDoItem>()).ToList();
        }

        private async Task<HttpResponseMessage> Call(Func<Task<HttpResponseMessage>> action)
        {
            var response = await action();
            switch (response.StatusCode)
            {
                case HttpStatusCode.Created:
                case HttpStatusCode.OK:
                case HttpStatusCode.NoContent:
                {
                    break;
                }
                case HttpStatusCode.BadRequest:
                {
                    throw new ArgumentNullException();
                }
                default:
                {
                    throw new InvalidOperationException();
                }
            }
            return response;
        }

        public async Task Add()
        {
            var description = _console.GetDescription();
            var status = _console.GetToDoItemStatus();
            var todoItem = new ToDoItem {Description = description, Status = (ToDoItemStatus)status};
            await Call(() => _client.Post(todoItem));
            _console.PrintDoneMessage();
            await List();
        }

        public async Task Edit()
        {
            var toDoList = await GetActualToDoList();
            var taskId = ChooseTaskId(toDoList);
            _console.PrintNewDescriptionRequest();
            var newDescription = _console.ReadLine();
            var patchDoc = new JsonPatchDocument<ToDoItem>();
            patchDoc.Replace(e => e.Description, newDescription);

            await Call(() => _client.Patch(taskId, patchDoc));
            _console.PrintDoneMessage();
            await List(); 
        }

        public async Task Complete()
        {
            var toDoList = await GetActualToDoList();
            var taskId = ChooseTaskId(toDoList);
            var patchDoc = new JsonPatchDocument<ToDoItem>();
            patchDoc.Replace(e => e.Status, ToDoItemStatus.Complete);
            await Call(() => _client.Patch(taskId, patchDoc));
            _console.PrintDoneMessage();
            await List();
        }

        public async Task Incomplete()
        {
            var toDoList = await GetActualToDoList();
            var taskId = ChooseTaskId(toDoList);
            var patchDoc = new JsonPatchDocument<ToDoItem>();
            patchDoc.Replace(e => e.Status, ToDoItemStatus.NotComplete);
            await Call(() => _client.Patch(taskId, patchDoc));
            _console.PrintDoneMessage();
            await List();
        }

        public async Task Delete()
        {
            var toDoList = await GetActualToDoList();
            var taskId = ChooseTaskId(toDoList);
            await Call(() => _client.Delete(taskId));
            _console.PrintDoneMessage();
            await List();
        }

        public int ChooseTaskId(IEnumerable<ToDoItem> toDoList)
        {
            _console.PrintTaskIdRequest();
            var input = _console.ReadLine();
            if (!int.TryParse(input, out var result))
            {
                throw new ArgumentNullException();
            }

            if (result >= 0 && result <= toDoList.Last().Id)
                return result;
            throw new ArgumentOutOfRangeException();
        }

        public Task Exit()
        {
            IsWorking = false;
            return Task.CompletedTask;
        }

        public async Task List()
        {
            var toDoList = await GetActualToDoList();
            var tableBuilder = new TableBuilder(toDoList);
            var table = tableBuilder.FormTable();
            _console.RenderTable(table);
        }

        public Task Error()
        {
            _console.PrintErrorMessage();
            return Task.CompletedTask;
        }
    }
}
