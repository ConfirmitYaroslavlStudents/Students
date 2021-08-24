using System.Collections.Generic;

namespace ToDoApiDependencies
{
    public class PatchToDoTask
    {
        private string _text;
        private bool _isDone;
        private List<string> _tags;
        public string Text 
        {
            get => _text;
            set
            {
                IsPatchContainsText = true;
                _text = value;
            }
        }
        public bool IsDone 
        {
            get => _isDone;
            set
            {
                IsPatchContainsIsDone = true;
                _isDone = value;
            }
        }
        public List<string> Tags
        {
            get => _tags;
            set
            {
                IsPatchContainsTags = true;
                _tags = value;
            }
        }
        public bool IsPatchContainsText { get; private set; }
        public bool IsPatchContainsIsDone { get; private set; }
        public bool IsPatchContainsTags { get; private set; }
    }
}
