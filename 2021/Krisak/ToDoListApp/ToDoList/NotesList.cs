using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ToDoList
{
    [Serializable]
    public class NotesList : ICreate, IRemove, IEdit,ISerialization, ISaveTXT
    {
        private List<ToDoList.INote> _list;
        public int Count
        {
            get { return _list.Count; }
            private set { }
        }

        public INote this[int i]
        {
            get
            {
                if (i >= Count || i < 0)
                    throw new IndexOutOfRangeException();
                return _list[i];
            }
            private set
            {
                if (i >= Count || i < 0)
                    throw new IndexOutOfRangeException();
                _list[i] = value;
            }
        }

        public NotesList()
        {
            _list = new List<INote>();
        }

        public void Add(INote newNote)
        {
            _list.Add(newNote);
        }

        public void Remove(int index)
        {
            _list.RemoveAt(index);
        }

        public void RemoveAll()
        {
            _list.Clear();
        }

        public void ChangeText(string text, int index)
        {
            _list[index].Text = text;
        }

        public void ChangeStatus(INote newNote, int indexChange)
        {
            newNote.Text = _list[indexChange].Text;
            _list[indexChange] = newNote;
        }

        public List<string> OutputNotes()
        {
            var newList = new List<string>();

            foreach (var item in _list)
                newList.Add(item.ToString());

            return newList;
        }

        public void SaveInTXT(TextWriter stream)
        {
            stream.WriteLine("\tMY NOTES");
            foreach (var item in OutputNotes())
                stream.WriteLine(item);
        }

        public void SaveSerialization()
        {
            BinaryFormatter binSave = new BinaryFormatter();
            using (var sw = new FileStream("save.dat", FileMode.Create, FileAccess.Write))
            {
                binSave.Serialize(sw, _list);
            }
        }

        public void LoadSerialization()
        {
            BinaryFormatter binSave = new BinaryFormatter();
            using (var sr = new FileStream("save.dat", FileMode.Open, FileAccess.Read))
            {
                _list = (List<INote>) binSave.Deserialize(sr);
            }
        }
    }
}
