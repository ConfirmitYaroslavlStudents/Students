namespace FilmService.KindsOfStatements
{
    public abstract class Statement
    {
        internal string postfix;
        public abstract void Serialize(string path, DataStore currentData);
        public abstract DataStore Deserialize(string path);
    }
}
