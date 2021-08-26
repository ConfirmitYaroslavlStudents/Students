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
    public class SelectedToDoItemsLogicalOperatorAndControllerTest
    {
        [Fact]
        public async Task GetSelectedToDoItems_NotSelectTag_ShouldReturnSelectedToDoItemsCount0()
        {
            //arrange
            var selectedToDoItemsLogicalOperatoinAndController = new SelectedToDoItemsLogicalOperatorAndController(CreateToDoContext(
                                                                             new List<SelectedTag>()
                                                                            ));
            //act
            var selectedToDoItems = (await selectedToDoItemsLogicalOperatoinAndController.GetToDoItems()).Value.ToList();
            //assert
            Assert.Empty(selectedToDoItems);
        }
        [Fact]
        public async Task GetSelectedToDoItems_SelectTagWork_ShouldReturnSelectedToDoItemsCount2()
        {
            //arrange
            var selectedToDoItemsLogicalOperatoinAndController = new SelectedToDoItemsLogicalOperatorAndController(CreateToDoContext(
                                                                             new List<SelectedTag>
                                                                             {
                                                                                 new SelectedTag{TagId = 1}
                                                                             }
                                                                            ));
            //act
            var selectedToDoItems = (await selectedToDoItemsLogicalOperatoinAndController.GetToDoItems()).Value.ToList();
            //assert
            Assert.Equal(2, selectedToDoItems.Count);
            Assert.Contains(selectedToDoItems, e => e.Description == "test tag work");
            Assert.Contains(selectedToDoItems, e => e.Description == "test tag work and home");
        }
        [Fact]
        public async Task GetSelectedToDoItems_SelectTagHome_ShouldReturnSelectedToDoItemsCount2()
        {
            //arrange
            var selectedToDoItemsLogicalOperatoinAndController = new SelectedToDoItemsLogicalOperatorAndController(CreateToDoContext(
                                                                             new List<SelectedTag>
                                                                             {
                                                                                 new SelectedTag{TagId = 2}
                                                                             }
                                                                            ));
            //act
            var selectedToDoItems = (await selectedToDoItemsLogicalOperatoinAndController.GetToDoItems()).Value.ToList();
            //assert
            Assert.Equal(2, selectedToDoItems.Count);
            Assert.Contains(selectedToDoItems, e => e.Description == "test tag home");
            Assert.Contains(selectedToDoItems, e => e.Description == "test tag work and home");
        }
        [Fact]
        public async Task GetSelectedToDoItems_SelectTagWorkAndHome_ShouldReturnSelectedToDoItemsCount2()
        {
            //arrange
            var selectedToDoItemsLogicalOperatoinAndController = new SelectedToDoItemsLogicalOperatorAndController(CreateToDoContext(
                                                                             new List<SelectedTag>
                                                                             {
                                                                                 new SelectedTag{TagId = 1},
                                                                                 new SelectedTag{TagId = 2}
                                                                             }
                                                                            ));
            //act
            var selectedToDoItems = (await selectedToDoItemsLogicalOperatoinAndController.GetToDoItems()).Value.ToList();
            //assert
            Assert.Single(selectedToDoItems);
            Assert.Contains(selectedToDoItems, e => e.Description == "test tag work and home");
        }
        public ToDoContext CreateToDoContext(List<SelectedTag> selectedTag)
        {
            var serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase()
                                                         .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<ToDoContext>();

            builder.UseInMemoryDatabase(Guid.NewGuid().ToString()).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            builder.UseInternalServiceProvider(serviceProvider);

            var toDoContext = new ToDoContext(builder.Options);

            var toDoItems = new List<ToDoItem>
            {
                new ToDoItem { Description = "test tag work"}, // id = 1 
                new ToDoItem { Description = "test tag home"}, // id = 2
                new ToDoItem { Description = "test tag work and home"}, // id = 3
                new ToDoItem { Description = "test tag"} // id = 4
            };
            var tags = new List<Tag>
            {
                new Tag { Name = "work"}, //id = 1
                new Tag { Name = "home"} // id = 2
            };
            var tagToDoItems = new List<TagToDoItem>
            {
                new TagToDoItem { ToDoItemId = 1, TagId = 1},
                new TagToDoItem { ToDoItemId = 2, TagId = 2},
                new TagToDoItem { ToDoItemId = 3, TagId = 1},
                new TagToDoItem { ToDoItemId = 3, TagId = 2}
            };

            toDoContext.AddRange(selectedTag);
            toDoContext.AddRange(tags);
            toDoContext.AddRange(toDoItems);
            toDoContext.AddRange(tagToDoItems);

            toDoContext.SaveChanges();

            return toDoContext;
        }
    }
}
