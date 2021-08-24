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
    public class TagsControllerTest
    {
        [Fact]
        public async Task GetTags_ShouldReturnTags()
        {
            //arrange
            var controller = new TagsController(CreateToDoContext(
                                                                    new List<Tag>
                                                                    {
                                                                        new Tag{Name = "test1"},
                                                                        new Tag{Name = "test2"}
                                                                    }
                                                                        ));
            //act
            var listTags = (await controller.GetTags()).Value.ToList();

            //assert
            Assert.Equal(2, listTags.Count);
            Assert.Equal(1, listTags[0].Id);
            Assert.Equal("test1", listTags[0].Name);
            Assert.Equal(2, listTags[1].Id);
            Assert.Equal("test2", listTags[1].Name);
        }
        [Fact]
        public async Task GetTag_ShouldReturnTag()
        {
            //arrange
            var controller = new TagsController(CreateToDoContext(
                                                                    new List<Tag>
                                                                    {
                                                                        new Tag{Name = "test1"},
                                                                        new Tag{Name = "test2"}
                                                                    }
                                                                        ));
            //act
            var tag = (await controller.GetTag(1)).Value;

            //assert
            Assert.Equal(1, tag.Id);
            Assert.Equal("test1", tag.Name);
        }
        [Fact]
        public async Task PostTag_NameTest_ShouldPostTag()
        {
            //arrange
            var toDoContext = CreateToDoContext(new List<Tag>());

            var controller = new TagsController(toDoContext);

            //act
            await controller.PostTag(new Tag { Name = "Test" });

            var actualTeg = toDoContext.Tags.Find(1L);
            //assert
            Assert.Equal(1, actualTeg.Id);
            Assert.Equal("Test", actualTeg.Name);
        }
        [Fact]
        public async Task PutTag_NewNameTestPut_ShouldPutTag()
        {
            //arrange
            var toDoContext = CreateToDoContext(new List<Tag>
                                                {
                                                }
                                                    );

            var tag = new Tag { Id = 1, Name = "Test" };
            toDoContext.Tags.Add(tag);
            var controller = new TagsController(toDoContext);
            //act
            tag.Name = "TestPost";
            await controller.PutTag(1, tag);

            var actualTeg = toDoContext.Tags.Find(1L);
            //assert
            Assert.Equal(1, actualTeg.Id);
            Assert.Equal("TestPost", actualTeg.Name);
        }
        [Fact]
        public async void DeleteTag_ShouldDeleteTag()
        {
            //arrange
            var toDoContext = CreateToDoContext(
                                                    new List<Tag>
                                                    {
                                                        new Tag{Name = "test1"}
                                                    }
                                                        );

          

            var controller = new TagsController(toDoContext);
            //act
            await controller.DeleteTag(1);

            //assert
            Assert.Null(toDoContext.Tags.Find(1L));
        }
        public ToDoContext CreateToDoContext(List<Tag> tags)
        {
            var serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase()
                                                         .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<ToDoContext>();

            builder.UseInMemoryDatabase(Guid.NewGuid().ToString()).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            builder.UseInternalServiceProvider(serviceProvider);

            var toDoContext = new ToDoContext(builder.Options);

            toDoContext.AddRange(tags);

            toDoContext.SaveChanges();

            return toDoContext;
        }
    }
}
