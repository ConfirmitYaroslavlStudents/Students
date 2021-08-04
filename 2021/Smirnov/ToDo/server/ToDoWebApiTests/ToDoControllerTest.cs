using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using ToDoWebApi.Controllers;
using ToDoWebApi.Database;
using ToDoWebApi.Models;

namespace ToDoWebApiTests
{
    public class ToDoControllerTest
    {
        [Fact]
        public async void AddToDoItem_DescriptionTest()
        {
            //arrange
            var toDoContext = createToDoContext();

            var controller = new ToDoItemsController(toDoContext);
            //act
            await controller.PostToDoItem(new ToDoItem { Description = "Test" });
            //assert
            Assert.Equal(1, toDoContext.ToDoItems.Find(1L).Id);
            Assert.Equal("Test", toDoContext.ToDoItems.Find(1L).Description);
            Assert.Equal(ToDoItemStatus.NotDone, toDoContext.ToDoItems.Find(1L).Status);
        }
        [Fact]
        public void GetToDoItems_ShouldReturnToDoItems()
        {
            //arrange
            var toDoContext = createToDoContext();
            toDoContext.AddRange(new List<ToDoItem>{new ToDoItem {Description = "test1"}, new ToDoItem {Description = "test2", Status = ToDoItemStatus.Done}});

            var controller = new ToDoItemsController(toDoContext);
            //act
            var listToDoItem = controller.GetToDoItems().Result.Value;

            //assert
            foreach (var toDoItem in listToDoItem)
            {
                Assert.Equal(1, toDoItem.Id);
                Assert.Equal("test1", toDoItem.Description);
                Assert.Equal(ToDoItemStatus.NotDone, toDoItem.Status);
                Assert.Equal(2, toDoItem.Id);
                Assert.Equal("test2", toDoItem.Description);
                Assert.Equal(ToDoItemStatus.Done, toDoItem.Status);
            }
        }
        [Fact]
        public void GetToDoItem_ShouldReturnToDoItem()
        {
            //arrange
            var toDoContext = createToDoContext();
            toDoContext.AddRange(new List<ToDoItem> { new ToDoItem { Description = "test1" }, new ToDoItem { Description = "test2", Status = ToDoItemStatus.Done } });

            var controller = new ToDoItemsController(toDoContext);
            //act
            var toDoItem = controller.GetToDoItem(1).Result.Value;

            //assert
            Assert.Equal(1, toDoItem.Id);
            Assert.Equal("test1", toDoItem.Description);
            Assert.Equal(ToDoItemStatus.NotDone, toDoItem.Status);
        }
        [Fact]
        public async void PatchToDoItem_NewDescriptionTestPatch()
        {
            //arrange
            var toDoContext = createToDoContext();
            toDoContext.Add(new ToDoItem { Description = "test1" });
            var controller = new ToDoItemsController(toDoContext);

            var patchToDoItemDto = new PatchToDoItemDto { Description = "TestPatch"};
            patchToDoItemDto.SetHasProperty("description");
            //act
            await controller.PatchToDoItem(1, patchToDoItemDto);

            //assert
            Assert.Equal(1, toDoContext.ToDoItems.Find(1L).Id);
            Assert.Equal("TestPatch", toDoContext.ToDoItems.Find(1L).Description);
            Assert.Equal(ToDoItemStatus.NotDone, toDoContext.ToDoItems.Find(1L).Status);
        }
        [Fact]
        public async void PatchToDoItem_NewStatusDone()
        {
            //arrange
            var toDoContext = createToDoContext();
            toDoContext.Add(new ToDoItem { Description = "test1" });
            var controller = new ToDoItemsController(toDoContext);

            var patchToDoItemDto = new PatchToDoItemDto {Status = ToDoItemStatus.Done};
            patchToDoItemDto.SetHasProperty("status");
            //act
            await controller.PatchToDoItem(1, patchToDoItemDto);

            //assert
            Assert.Equal(1, toDoContext.ToDoItems.Find(1L).Id);
            Assert.Equal("test1", toDoContext.ToDoItems.Find(1L).Description);
            Assert.Equal(ToDoItemStatus.Done, toDoContext.ToDoItems.Find(1L).Status);
        }
        [Fact]
        public async void PatchToDoItem_NewDescriptionTestPatch_newStatusDone()
        {
            //arrange
            var toDoContext = createToDoContext();
            toDoContext.Add(new ToDoItem { Description = "test1" });
            var controller = new ToDoItemsController(toDoContext);

            var patchToDoItemDto = new PatchToDoItemDto { Description = "TestPatch", Status = ToDoItemStatus.Done };
            patchToDoItemDto.SetHasProperty("description");
            patchToDoItemDto.SetHasProperty("status");
            //act
            await controller.PatchToDoItem(1, patchToDoItemDto);

            //assert
            Assert.Equal(1, toDoContext.ToDoItems.Find(1L).Id);
            Assert.Equal("TestPatch", toDoContext.ToDoItems.Find(1L).Description);
            Assert.Equal(ToDoItemStatus.Done, toDoContext.ToDoItems.Find(1L).Status);
        }
        [Fact]
        public async void DeleteToDoItem_ShouldDeleteToDoItem()
        {
            //arrange
            var toDoContext = createToDoContext();

            toDoContext.Add(new ToDoItem { Description = "test1" });

            var controller = new ToDoItemsController(toDoContext);
            //act
            await controller.DeleteToDoItem(1);

            //assert
            Assert.Null(toDoContext.ToDoItems.Find(1L));

        }
        private ToDoContext createToDoContext()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<ToDoContext>();

            builder.UseInMemoryDatabase("todo_db");
            builder.UseInternalServiceProvider(serviceProvider);

            return new ToDoContext(builder.Options);
        }

    }
}
