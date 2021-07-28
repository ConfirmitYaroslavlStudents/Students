using System;
using System.Collections;
using System.Collections.Generic;
using ToDoListLib.Models;
using ToDoListLib.SaveAndLoad;
using Xunit;

namespace ToDoListLibTests
{
    public class TestDataClass : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[]{new SaveAndLoadFromFile()},
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
    }
    public class SaveAndLoadIntegrationTest // Возник вопрос
    {

        [Theory]
        [ClassData(typeof(TestDataClass))]
        public void CreateToDoList_Save_Load_CompareCollections(ISaveAndLoad saveAndLoad)
        {
            var toDoList = new List<Task>();
            var random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                toDoList.Add(new Task{Id = random.Next(), Description = random.Next().ToString(), Status = (TaskStatus)random.Next(0,1)});
            }

            saveAndLoad.Save(toDoList);
            var newToDoList = (List<Task>)saveAndLoad.Load();

            for (int i = 0; i < 1000; i++)
            {
                Assert.Equal(toDoList[i].Id, newToDoList[i].Id);
                Assert.Equal(toDoList[i].Description, newToDoList[i].Description);
                Assert.Equal(toDoList[i].Status, newToDoList[i].Status);
            }
        }

    }
}
