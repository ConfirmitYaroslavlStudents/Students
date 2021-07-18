using System;

namespace ToDoList
{
    [Serializable]
    public class Note : INote
    {
        private string _text;

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public Note() : this("") { }

        public Note(string text)
        {
            _text = text;
        }

        public override string ToString()
        {
            return Text;
        }
    }

    [Serializable]
    public class NoteCompleted: Note
    {
        string _flag = "X"; 

        public NoteCompleted() : base() { }
        public NoteCompleted(string text) : base(text) { }

        public override string ToString()
        {
            return _flag+ " "+ Text;
        }
    }
}
