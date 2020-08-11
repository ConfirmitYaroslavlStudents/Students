using BillSplitter.Data;
using BillSplitter.Models;
using Xunit;

namespace BillSplitterTests
{
    public class PositionRepositoryTests
    {
        [Fact]
        public void AddPosition_AddNewPositionInDb()
        {
            using var db = new InMemoryContextBuilder().Build();
            var uow = new UnitOfWork(db);

            var repo = uow.Positions;

            var position = new Position()
            {
                Id = 1
            };

            repo.Add(position);
            uow.Save();

            Assert.True(repo.GetById(1) != null);
        }

        [Fact]
        public void GetById_ReturnsRightPosition()
        {
            using var db = new InMemoryContextBuilder().Build();
            var uow = new UnitOfWork(db);

            var repo = uow.Positions;

            var position1 = new Position()
            {
                Id = 1
            };
            var position2 = new Position()
            {
                Id = 2
            };

            repo.Add(position1);
            repo.Add(position2);

            uow.Save();

            var expected = position1;
            var actual = repo.GetById(1);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetById_PositionExists_ReturnsNotNull()
        {
            using var db = new InMemoryContextBuilder().Build();
            var uow = new UnitOfWork(db);

            var repo = uow.Positions;

            var position1 = new Position
            {
                Id = 1
            };
            var position2 = new Position
            {
                Id = 2
            };

            repo.Add(position1);
            repo.Add(position2);

            uow.Save();

            var actual = repo.GetById(1);

            Assert.NotNull(actual);
        }

        [Fact]
        public void GetById_PositionDoesntExists_ReturnsNull()
        {
            using var db = new InMemoryContextBuilder().Build();
            var uow = new UnitOfWork(db);

            var repo = uow.Positions;

            var position1 = new Position
            {
                Id = 1
            };

            repo.Add(position1);

            uow.Save();

            var actual = repo.GetById(2);

            Assert.Null(actual);
        }

        [Fact]
        public void UpdateById_UpdatesPosition()
        {
            using var db = new InMemoryContextBuilder().Build();
            var uow = new UnitOfWork(db);

            var repo = uow.Positions;

            var position1 = new Position
            {
                Id = 1,
                Name = "a",
                Quantity = 1,
                Price = 1
            };

            repo.Add(position1);
            uow.Save();

            repo.UpdateById(1, new Position { Name = "b", Quantity = 2, Price = 3 });
            uow.Save();

            var actual = repo.GetById(1);
            var expected = new Position {Name = "b", Quantity = 2, Price = 3};

            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Price, actual.Price);
            Assert.Equal(expected.Quantity, actual.Quantity);
        }

        [Fact]
        public void DeleteById_DeletesRightPosition()
        {
            using var db = new InMemoryContextBuilder().Build();
            var uow = new UnitOfWork(db);

            var repo = uow.Positions;

            var position1 = new Position
            {
                Id = 1
            };

            repo.Add(position1);
            uow.Save();

            repo.DeleteById(1);
            uow.Save();

            var actual = repo.GetById(1);

            Assert.Null(actual);
        }
    }
}
