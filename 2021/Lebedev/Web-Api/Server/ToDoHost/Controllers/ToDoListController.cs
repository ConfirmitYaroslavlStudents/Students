using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using MyTODO;

namespace ToDoHost.Controllers
{
    [ApiController]
    [Route("todolist")]
    public class ToDoListController : ControllerBase
    {
        public ToDoList Todo { get; set; }
        private readonly ILogger<ToDoListController> _logger;
        private ToDoFileManager _manager = new ToDoFileManager(new FileInfo($"TODOsave.txt"));

        public ToDoListController(ILogger<ToDoListController> logger)
        {
            Todo = new ToDoList(_manager.Read());
            _logger = logger;
        }

        private void TestInitialize()
        {
            _manager = new ToDoFileManager(new FileInfo($"testsave.txt"));
            Todo = new ToDoList(_manager.Read());
        }

        [HttpGet]
        public IEnumerable<ToDoItem> GetList(bool test = false)
        {
            if(test)
                TestInitialize();
            _logger.Log(logLevel: LogLevel.Information, "Sent full todo list");
            return Todo;
        }

        [HttpGet("{id:int}")]
        public ToDoItem GetItem(int id, bool test = false)
        {
            if(test)
                TestInitialize();
            if (id >= 0 && id < Todo.Count)
            {
                _logger.Log(logLevel: LogLevel.Information, $"Sent todo item {id}");
                return Todo[id];
            }

            _logger.Log(logLevel: LogLevel.Information, $"Todo item {id} not sent");
            throw new ArgumentOutOfRangeException("wrong id");
        }

        [HttpPatch]
        public string PatchItem([FromBody] ToDoItem item, bool test = false)
        {
            if(test)
                TestInitialize();
            if (item.Completed!=null)
                Todo[(int)item.Id].Completed = item.Completed;
            if (item.Deleted != null)
                Todo[(int)item.Id].Deleted = item.Deleted;
            if (!string.IsNullOrEmpty(item.Name))
                Todo[(int)item.Id].ChangeName(item.Name);
            if (item.Tags != null)
                Todo[(int) item.Id].Tags = item.Tags;
            _manager.Save(Todo);
            _logger.Log(logLevel: LogLevel.Information, $"Patch item {item.Id} completed");
            return $"Patch item {item.Id} completed";
        }

        [HttpPost]
        public string PostItem([FromBody] ToDoItem item, bool test = false)
        {
            if(test)
                TestInitialize();
            Todo.Add(item.Name);
            if ((bool)item.Completed)
                Todo[^1].SetCompletedTrue();
            if ((bool)item.Deleted)
                Todo[^1].SetDeletedTrue();
            if (item.Tags != null)
                Todo[^1].Tags = item.Tags;
            _manager.Save(Todo);
            _logger.Log(logLevel: LogLevel.Information, $"Posted todo item {Todo.Count - 1} created");
            return "Post Completed";
        }

        [HttpDelete("deletelist")]
        public string DeleteList(bool test = false)
        {
            if(test)
                TestInitialize();
            Todo = new ToDoList();
            _manager.Save(Todo);
            _logger.Log(logLevel: LogLevel.Information, $"Deleted todo list");
            return "Delete Completed";
        }

        [HttpDelete("{id:int}")]
        public string DeleteItem(int id, bool test = false)
        {
            if(test)
                TestInitialize();
            Todo[id].SetDeletedTrue();
            _manager.Save(Todo);
            _logger.Log(logLevel: LogLevel.Information, $"Deleted todo item {id}");
            return "Delete Completed";
        }
    }
}