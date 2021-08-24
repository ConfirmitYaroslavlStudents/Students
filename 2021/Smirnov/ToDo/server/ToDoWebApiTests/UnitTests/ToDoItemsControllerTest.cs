using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using ToDoWebApi.Controllers;
using ToDoWebApi.Database;
using ToDoWebApi.Models;
using ToDoWebApi.Models.Patch;
using System.Linq;

namespace ToDoWebApiTests.UnitTests
{
    public class ToDoItemsControllerTest
    {
        [Fact]
        public async void AddToDoItem_DescriptionTest()
        {
            //arrange
            var toDoContext = CreateToDoContext();

            var controller = new ToDoItemsController(toDoContext);
            //act
            await controller.PostToDoItem(new ToDoItem { Description = "Test" });
            //assert
            Assert.Equal(1, toDoContext.ToDoItems.Find(1L).Id);
            Assert.Equal("Test", toDoContext.ToDoItems.Find(1L).Description);
            Assert.Equal(ToDoItemStatus.NotDone, toDoContext.ToDoItems.Find(1L).Status);
        }
        [Fact]
        public async void GetToDoItems_ShouldReturnToDoItems()
        {
            //arrange
            var toDoContext = CreateToDoContext();
            toDoContext.AddRange(new List<ToDoItem>{new ToDoItem {Description = "test1"},
                                 new ToDoItem {Description = "test2", Status = ToDoItemStatus.Done}});

            toDoContext.SaveChanges();

            var controller = new ToDoItemsController(toDoContext);
            //act
            var listToDoItem = (await controller.GetToDoItems()).Value.ToList();

            //assert
            Assert.Equal(2, listToDoItem.Count);
            Assert.Equal(1, listToDoItem[0].Id);
            Assert.Equal("test1", listToDoItem[0].Description);
            Assert.Equal(ToDoItemStatus.NotDone, listToDoItem[0].Status);
            Assert.Equal(2, listToDoItem[1].Id);
            Assert.Equal("test2", listToDoItem[1].Description);
            Assert.Equal(ToDoItemStatus.Done, listToDoItem[1].Status);
        }
        [Fact]
        public void GetToDoItem_ShouldReturnToDoItem()
        {
            //arrange
            var toDoContext = CreateToDoContext();
            toDoContext.AddRange(new List<ToDoItem> { new ToDoItem { Description = "test1" }, 
                                 new ToDoItem { Description = "test2", Status = ToDoItemStatus.Done } });

            toDoContext.SaveChanges();

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
            var toDoContext = CreateToDoContext();
            toDoContext.Add(new ToDoItem { Description = "test1" });

            toDoContext.SaveChanges();

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
            var toDoContext = CreateToDoContext();
            toDoContext.Add(new ToDoItem { Description = "test1" });

            toDoContext.SaveChanges();

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
            var toDoContext = CreateToDoContext();
            toDoContext.Add(new ToDoItem { Description = "test1" });

            toDoContext.SaveChanges();

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
            var toDoContext = CreateToDoContext();

            toDoContext.Add(new ToDoItem { Description = "test1" });

            toDoContext.SaveChanges();

            var controller = new ToDoItemsController(toDoContext);
            //act
            await controller.DeleteToDoItem(1);

            //assert
            Assert.Null(toDoContext.ToDoItems.Find(1L));

        }
        private ToDoContext CreateToDoContext()
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
