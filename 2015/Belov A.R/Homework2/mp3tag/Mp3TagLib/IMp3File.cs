namespace Mp3TagLib
{
    public interface IMp3File
    {
        string Name { get; }
        string Path { get; }
        void SetTags(Mp3Tags tags);
        Mp3Tags GetTags();
        void ChangeName(string newName);
        void Save();
        Mp3Memento GetMemento();

        void SetMemento(Mp3Memento memento);
    }
}
