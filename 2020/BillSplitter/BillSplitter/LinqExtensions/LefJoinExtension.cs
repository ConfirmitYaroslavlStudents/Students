using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BillSplitter.LinqExtensions
{
    public static class LefJoinExtension
    {
        public static IEnumerable<LeftJoinResult<TOuter, TInner>> LeftJoin<TOuter, TInner, TKey>(
            this IEnumerable<TOuter> outer,
            IEnumerable<TInner> inner,
            Func<TOuter, TKey> outerKeySelector,
            Func<TInner, TKey> innerKeySelector)
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
                (joined, innerEntity) => 
                    new LeftJoinResult<TOuter, TInner>
                    {
                        OuterEntity = joined.OuterEntity,
                        InnerEntity = innerEntity

                    });

            return result;
        }
    }
}