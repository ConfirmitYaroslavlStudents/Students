using System;
using System.Collections.Generic;
using System.Linq;

namespace BillSplitter.LinqExtensions
{
    public static class LefJoinExtension
    {
        public static IEnumerable<TResult> LeftJoin<TOuter, TInner, TKey, TResult>(
            this IEnumerable<TOuter> outer,
            IEnumerable<TInner> inner,
            Func<TOuter, TKey> outerKeySelector,
            Func<TInner, TKey> innerKeySelector,
            Func<TOuter, TInner, TResult> resultSelector)
        {
            var groupJoin = outer.GroupJoin(
                inner, 
                outerKeySelector, 
                innerKeySelector,
                (outerEntity, innerCollection) => new
                {
                    OuterEntity = outerEntity,
                    InnerCollection = innerCollection.DefaultIfEmpty()
                });

            var result = groupJoin.SelectMany(
                joined => joined.InnerCollection,
                (joined, innerEntity) => resultSelector(joined.OuterEntity, innerEntity));

            return result;
        }
    }
}