using System;

namespace Refactoring.StatementTools
{
    public interface IStatementFormater
    {
        void GetStatement(Action<object> delegateWriter, Statement statement);
    }
}
