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

        public string GetStatement(IStatement statement, Customer customer)
        {
            foreach (var currentStatement in _listOfStatements)
            {
                if (!statement.GetType().Equals(currentStatement.GetType())) continue;

                Console.WriteLine("TYPES TYPES:");
                Console.WriteLine(statement.GetType());
                Console.WriteLine(currentStatement.GetType());
                return currentStatement.GetStatement(customer);
            }

            //TODO!!!
            throw new Exception();
        }
    }
}
