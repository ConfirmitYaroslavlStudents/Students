using System.IO;

namespace RefactoringSample
{
    public class CompositeStatement
    {
        public string StringStatement;
        public MemoryStream JsonStatement;

        public CompositeStatement(Statement statement)
        {
            StringStatement = new StringStatementGenerator().Create(statement);
            JsonStatement = new JsonStatementGenerator().Create(statement);
        }
    }
}
