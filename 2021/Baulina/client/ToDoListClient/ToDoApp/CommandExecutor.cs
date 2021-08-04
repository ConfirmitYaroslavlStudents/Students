using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using InputOutputManagers;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;

namespace ToDoApp
{
    public class CommandExecutor : IExecute
    {
        private readonly IConsoleExtended _console;
        private readonly HttpClient _client;
        private const string RequestUri = "todoItems";
        public bool IsWorking { get; private set; }

        public CommandExecutor(IConsoleExtended console, HttpClient client)
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
            catch (ArgumentNullException)
            {
                _console.PrintErrorMessage();
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
            var response = await Call(() => _client.GetAsync(RequestUri));
            var responseString = await response.Content.ReadAsStringAsync();
            return (JsonConvert.DeserializeObject<IEnumerable<ToDoItem>>(responseString) ?? Array.Empty<ToDoItem>()).ToList();
        }

        private async Task<HttpResponseMessage> Call(Func<Task<HttpResponseMessage>> action)
        {
            var response = await action();
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                {
                    break;
                }
                case HttpStatusCode.NotFound:
                {
                    throw new ArgumentOutOfRangeException();
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
            await Call(() => _client.PostAsync(RequestUri, RequestContentHelper.GetStringContent(description)));
            _console.PrintDoneMessage();
            await List();
        }

        public async Task Edit()
        {
            var toDoList = await GetActualToDoList();
            var taskNumber = ChooseTaskNumber(toDoList);
            _console.PrintNewDescriptionRequest();
            var newDescription = _console.ReadLine();

            var patchDoc = new JsonPatchDocument<ToDoItem>();
            patchDoc.Replace(e => e.Description, newDescription);

            await Call(() => _client.PatchAsync(RequestUri + $"/{taskNumber}", RequestContentHelper.GetPatchStringContent(patchDoc)));
            _console.PrintDoneMessage();
            await List();
        }

        public async Task Complete()
        {
            var toDoList = await GetActualToDoList();
            var taskNumber = ChooseTaskNumber(toDoList);
            var patchDoc = new JsonPatchDocument<ToDoItem>();
            patchDoc.Replace(e => e.IsComplete, true);
            await Call(() => _client.PatchAsync(RequestUri + $"/{taskNumber}", RequestContentHelper.GetPatchStringContent(patchDoc)));
            _console.PrintDoneMessage();
            await List();
        }

        public async Task Delete()
        {
            var toDoList = await GetActualToDoList();
            var taskNumber = ChooseTaskNumber(toDoList);
            await Call(() => _client.DeleteAsync(RequestUri + $"/{taskNumber}"));
            _console.PrintDoneMessage();
            await List();
        }

        public int ChooseTaskNumber(IEnumerable<ToDoItem> toDoList)
        {
            _console.PrintTaskNumberRequest();
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
            var table = tableBuilder.FormATable();
            _console.RenderTable(table);
        }

        public Task Error()
        {
            _console.PrintErrorMessage();
            return Task.CompletedTask;
        }
    }
}
