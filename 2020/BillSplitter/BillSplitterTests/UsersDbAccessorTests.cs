using BillSplitter.Data;
using BillSplitter.Models;
using System;
using System.Linq;
using Xunit;

namespace BillSplitterTests
{
    public class UsersDbAccessorTests
    {
        [Fact]
        public void AddUser_AddMewUserInDb()
        {
            using var db = InMemoryContextBuilder.Build();

            var accessor = new UserRepository(db);

            var user = new User()
            {
                Id = 1
            };

            accessor.Add(user);

            Assert.True(db.Users.Contains(user));
        }

        [Fact]
        public void GetUserByName_ReturnRightUser()
        {
            using var db = InMemoryContextBuilder.Build();

            var accessor = new UserRepository(db);

            var user = new User()
            {
                Name = "name"
            };

            db.Add(user);
            db.SaveChanges();

            var actual = accessor.GetByName("name");

            Assert.Equal(user, actual);
        }

        [Fact]
        public void GetUserByName_ReturnNull()
        {
            using var db = InMemoryContextBuilder.Build();

            var accessor = new UserRepository(db);

            var user = new User()
            {
                Name = "name"
            };

            db.Add(user);
            db.SaveChanges();

            var actual = accessor.GetByName("a");

            Assert.Null(actual);
        }
    }
}
