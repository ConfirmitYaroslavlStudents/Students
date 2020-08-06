namespace BillSplitter.LinqExtensions
{
    public class LeftJoinResult<TOuter, TInner>
    {
        public TOuter OuterEntity { get; set; }
        public TInner InnerEntity { get; set; }
    }
}