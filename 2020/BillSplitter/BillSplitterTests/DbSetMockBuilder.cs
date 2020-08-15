using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BillSplitterTests
{
    public static class DbSetMockBuilder 
    {
        public static DbSet<TEntity> BuildDbSet<TEntity>(List<TEntity> entities) 
            where TEntity : class
        {
            var queryable = entities.AsQueryable();
            var dbSet = new Mock<DbSet<TEntity>>();

            dbSet.As<IQueryable<TEntity>>()
                .Setup(d => d.Provider)
                .Returns(queryable.Provider);

            dbSet.As<IQueryable<TEntity>>()
                .Setup(d => d.Expression)
                .Returns(queryable.Expression);

            dbSet.As<IQueryable<TEntity>>()
                .Setup(d => d.ElementType)
                .Returns(queryable.ElementType);

            dbSet.Setup(d => d.Add(It.IsAny<TEntity>()))
                .Callback<TEntity>(entities.Add);

            dbSet.Setup(d => d.Remove(It.IsAny<TEntity>()))
                .Callback<TEntity>(e => entities.Remove(e));

            return dbSet.Object;
        }
    }
}