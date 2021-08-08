namespace ToDoApiDependencies
{
    public class PatchToDoTask
    {
        private string _text;
        private bool _isDone;
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
        public bool IsPatchContainsText { get; private set; }
        public bool IsPatchContainsIsDone { get; private set; }
    }
}
