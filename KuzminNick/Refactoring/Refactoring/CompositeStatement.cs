using System;
using System.Collections.Generic;
using VideoService;

namespace Refactoring
{
    public class CompositeStatement
    {
        private readonly List<IStatement> _listOfStatements = new List<IStatement>();

        public void AddStatement(IStatement statement)
        {
            _listOfStatements.Add(statement);
        }

        public void RemoveStatement(IStatement statement)
        {
            _listOfStatements.Remove(statement);
        }

        public string GetStatement(Customer customer)
        {
            var result = String.Empty;
            foreach (var currentStatement in _listOfStatements)
            {
                result += currentStatement.GetStatement(customer);
            }

            return result;
        }
    }
}
