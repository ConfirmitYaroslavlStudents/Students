using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatementArchitecture
{
    public class Statement:IStatement
    {
        public IStatement Statements;

        public Statement(IStatement statements)
        {
            Statements = statements;
        }

        public void Generate()
        {
            Statements.Generate();
        }
    }
}
