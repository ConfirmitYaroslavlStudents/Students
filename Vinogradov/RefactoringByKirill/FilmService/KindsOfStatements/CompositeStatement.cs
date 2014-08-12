using System.Collections.Generic;

namespace FilmService.KindsOfStatements
{
    public class CompositeStatement : Statement
    {
        private readonly IList<Statement> statements = new List<Statement>();

        public void AddStatement(Statement statement)
        {
            statements.Add(statement);
        }

        public override void Serialize(string path, DataStore currentData)
        {
            foreach (var item in statements)
            {
                item.Serialize(path,currentData);
            }
        }

        public override DataStore Deserialize(string path)
        {
            IList<DataStore> dataStores = new List<DataStore>();
            foreach (var item in statements)
            {
                dataStores.Add(item.Deserialize(path));
            }
            for (int i = 0; i < dataStores.Count-1; i++)
            {
                if (!dataStores[i].Equals(dataStores[i + 1]))
                {
                    return default(DataStore);
                }
            }
            if (statements.Count != 0)
            {
                return dataStores[0];
            }
            else
            {
                return default(DataStore);
            }
        }
    }
}
