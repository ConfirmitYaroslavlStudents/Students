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
    public class SelectedTagsControllerTest
    {
        [Fact]
        public async Task GetSelectedTags_ShouldReturnSelectedTags()
        {
            //arrange
            var controller = new SelectedTagsController(CreateToDoContext(
                                                                    new List<SelectedTag>
                                                                    {
                                                                        new SelectedTag{TagId = 1}
                                                                    }
                                                                        ));
            //act
            var listSelectedTags = (await controller.GetSelectedTags()).Value.ToList();

            //assert
            Assert.Single(listSelectedTags);
            Assert.Equal(1, listSelectedTags[0].Id);
            Assert.Equal(1, listSelectedTags[0].TagId);
        }
        [Fact]
        public async Task GetSelectedTag_ShouldReturnlistSelectedTag()
        {
            //arrange
            var controller = new SelectedTagsController(CreateToDoContext(
                                                                    new List<SelectedTag>
                                                                    {
                                                                        new SelectedTag{TagId = 1}
                                                                    }
                                                                        ));
            //act
            var tag = (await controller.GetSelectedTag(1)).Value;

            //assert
            Assert.Equal(1, tag.Id);
            Assert.Equal(1, tag.TagId);
        }
        [Fact]
        public async Task PostSelectedTags_NameTest_ShouldPostSelectedTags()
        {
            //arrange
            var toDoContext = CreateToDoContext(new List<SelectedTag>());

            var controller = new SelectedTagsController(toDoContext);

            //act
            await controller.PostSelectedTag(new SelectedTag { TagId = 1 });

            var actualSelectedTag = toDoContext.SelectedTags.Find(1L);
            //assert
            Assert.Equal(1, actualSelectedTag.Id);
            Assert.Equal(1, actualSelectedTag.TagId);
        }
        [Fact]
        public async Task PutSelectedTag_NewNameTestPut_ShouldPutSelectedTag()
        {
            //arrange
            var toDoContext = CreateToDoContext(new List<SelectedTag>
            {
            }
                                                    );

            var selectedTag = new SelectedTag { TagId = 1 };
            toDoContext.SelectedTags.Add(selectedTag);
            var controller = new SelectedTagsController(toDoContext);
            //act
            selectedTag.TagId = 2;
            await controller.PutSelectedTag(1, selectedTag);

            var actualTeg = toDoContext.SelectedTags.Find(1L);
            //assert
            Assert.Equal(1, actualTeg.Id);
            Assert.Equal(2, actualTeg.TagId);
        }
        [Fact]
        public async void DeleteSelectedTag_ShouldDeleteSelectedTag()
        {
            //arrange
            var toDoContext = CreateToDoContext(
                                                    new List<SelectedTag>
                                                    {
                                                        new SelectedTag{TagId = 1}
                                                    }
                                                        );



            var controller = new SelectedTagsController(toDoContext);
            //act
            await controller.DeleteSelectedTag(1);

            //assert
            Assert.Null(toDoContext.SelectedTags.Find(1L));
        }
        public ToDoContext CreateToDoContext(List<SelectedTag> selectedTag)
        {
            var serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase()
                                                         .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<ToDoContext>();

            builder.UseInMemoryDatabase(Guid.NewGuid().ToString()).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            builder.UseInternalServiceProvider(serviceProvider);

            var toDoContext = new ToDoContext(builder.Options);

            toDoContext.AddRange(selectedTag);

            toDoContext.SaveChanges();

            return toDoContext;
        }
    }
}
