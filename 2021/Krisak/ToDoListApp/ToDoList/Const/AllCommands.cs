using System;

namespace ToDoLibrary
{
    public static class AllCommands
    {
        public const string Add = "add";
        public const string Edit = "edit";
        public const string Toggle = "toggle";
        public const string Delete = "delete";
        public const string Rollback = "rollback";
        public const string Show = "show";
        public const string Exit = "bye";

        private const string AddCommandHelp = Add + " <notes>";
        private const string EditCommandHelp = Edit + " 00 <new notes>";
        private const string ToggleCommandHelp = Toggle + " 00 <0/1/2>";
        private const string DeleteCommandHelp = Delete + " 00";
        private const string RollbackCommandHelp = Rollback + " 00";

        public static readonly string[] Help = new string[]
        {
            $"{AddCommandHelp,-30}creating new note",
            $"{EditCommandHelp,-30}edit note",
            $"{ToggleCommandHelp,-30}toggle progress status note (0 - To do/ 1 - In progress/ 2 - Done)",
            $"{DeleteCommandHelp,-30}delete note",
            $"{RollbackCommandHelp,-30}rollback commands",
            $"{Show,-30}show all notes",
            $"{Exit,-30}exit"
        };
    }
}
