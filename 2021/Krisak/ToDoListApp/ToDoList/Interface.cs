namespace ToDoList
{
    public interface INote
    {
        string Text { get; set; }
        string ToString();
    }

    interface ICreate
    {
        void Add(INote newNote);
    }

    interface IRemove
    {
        void Remove(int index);
        void RemoveAll();
    }

    interface IEdit
    {
        void ChangeText(string text, int index);
        void ChangeStatus(INote newNote, int index);
    }

    interface ISaveTXT
    {
        void SaveInTXT(System.IO.TextWriter stream);
    }

    interface ISerialization
    {
        void SaveSerialization();
        void LoadSerialization();
    }
}
