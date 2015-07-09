namespace Refactoring.StatementTools
{
    public class StringStatementData : IStatementData
    {
        private string _data;

        public void SetData(object data)
        {
            _data = (string) data;
        }

        public object GetData()
        {
            return _data;
        }
    }
}
