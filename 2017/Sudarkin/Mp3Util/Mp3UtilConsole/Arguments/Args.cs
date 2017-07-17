namespace Mp3UtilConsole.Arguments
{
    public class Args
    {
        public string Mask { get; }
        public bool Recursive { get; }
        public ProgramAction Action { get; }

        public Args(string mask, bool resursive, ProgramAction action)
        {
            Mask = mask;
            Recursive = resursive;
            Action = action;
        }
    }
}