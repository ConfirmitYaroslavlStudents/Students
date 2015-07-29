namespace Mp3Tager
{
    internal class ParsedArgs
    {
        //[TODO] make it immutable
        //[TODO] command name into enum
        public string CommandName { get; set; }
        public string Path { get; set; }
        public string Pattern { get; set; }
        public string Tag { get; set; }
        public string NewTagValue { get; set; }
        public string CommandForHelp { get; set; }
    }
}
