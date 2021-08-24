using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using ToDoWebApi.Controllers;
using ToDoWebApi.Database;
using ToDoWebApi.Models;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace ToDoWebApiTests.UnitTests
{
    public class TagToDoItemsControllerTest
    {
        [Fact]
        public async Task GeTagToDoItems_ShouldReturnTagToDoItems()
        {
            //arrange
            var controller = new TagToDoItemsController(CreateToDoContext(
                                                                    new List<TagToDoItem>
                                                                    {
                                                                        new TagToDoItem{ToDoItemId = 1, TagId=1}
                                                                    }
                                                                        ));
            //act
            var listTagToDoItems = (await controller.GetTagToDoItems()).Value.ToList();

            //assert
            Assert.Single(listTagToDoItems);
            Assert.Equal(1, listTagToDoItems[0].Id);
            Assert.Equal(1, listTagToDoItems[0].ToDoItemId);
            Assert.Equal(1, listTagToDoItems[0].TagId);
        }
        [Fact]
        public async Task GetTagToDoItem_ShouldReturnTagToDoItem()
        {
            //arrange
            var controller = new TagToDoItemsController(CreateToDoContext(
                                                                    new List<TagToDoItem>
                                                                    {
                                                                        new TagToDoItem{ToDoItemId = 1, TagId=1}
                                                                    }
                                                                        ));
            //act
            var actualTagToDoItem = (await controller.GetTagToDoItem(1)).Value;

            //assert
            Assert.Equal(1, actualTagToDoItem.Id);
            Assert.Equal(1, actualTagToDoItem.ToDoItemId);
            Assert.Equal(1, actualTagToDoItem.TagId);
        }
        [Fact]
        public async Task PostTagToDoItem_NameTest_ShouldPostTagToDoItem()
        {
            //arrange
            var toDoContext = CreateToDoContext(new List<TagToDoItem>());

            var controller = new TagToDoItemsController(toDoContext);

            //act
            await controller.PostTagToDoItem(new TagToDoItem { ToDoItemId = 1, TagId = 1 });

            var actualTagToDoItem = (await controller.GetTagToDoItem(1)).Value;
            //assert
            Assert.Equal(1, actualTagToDoItem.Id);
            Assert.Equal(1, actualTagToDoItem.ToDoItemId);
            Assert.Equal(1, actualTagToDoItem.TagId);
        }
        [Fact]
        public async Task PutTagToDoItem_NewNameTestPut_ShouldPutTagToDoItem()
        {
            //arrange
            var toDoContext = CreateToDoContext(new List<TagToDoItem>());

            var tagToDoItem = new TagToDoItem { ToDoItemId = 1, TagId = 1 };
            toDoContext.TagToDoItems.Add(tagToDoItem);
            var controller = new TagToDoItemsController(toDoContext);
            //act
            tagToDoItem.TagId = 2;
            await controller.PutTagToDoItem(1, tagToDoItem);

            var actualTagToDoItem = toDoContext.TagToDoItems.Find(1L);
            //assert
            Assert.Equal(1, actualTagToDoItem.Id);
            Assert.Equal(1, actualTagToDoItem.ToDoItemId);
            Assert.Equal(2, actualTagToDoItem.TagId);
        }
        [Fact]
        public async void DeleteToDoItem_ShouldDeleteToDoItem()
        {
            //arrange
            var toDoContext = CreateToDoContext(
                                                    new List<TagToDoItem>
                                                    {
                                                       new TagToDoItem { ToDoItemId = 1, TagId = 1 }
                                                    }
                                                        );



            var controller = new TagToDoItemsController(toDoContext);
            //act
            await controller.DeleteTagToDoItem(1);

            //assert
            Assert.Null(toDoContext.TagToDoItems.Find(1L));
        }
        public ToDoContext CreateToDoContext(List<TagToDoItem> tagToDoItems)
        {
            var serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase()
                                                         .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<ToDoContext>();

            builder.UseInMemoryDatabase(Guid.NewGuid().ToString()).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            builder.UseInternalServiceProvider(serviceProvider);

            var toDoContext = new ToDoContext(builder.Options);

            toDoContext.AddRange(tagToDoItems);

            toDoContext.SaveChanges();

            return toDoContext;
        }
    }
}
