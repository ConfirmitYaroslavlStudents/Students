using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList
{
    public static class Commands
    {
        public const string Add = "add";
        public const string Edit = "edit";
        public const string Toggle = "toggle";
        public const string Delete = "delete";
        public const string Rollback = "rollback";
        public const string Show = "show";
        public const string Exit = "bye";

        static string AddCommandHelp = Add+ " <notes>";
        static string EditCommandHelp = Edit + " 00 <new notes>";
        static string ToggleCommandHelp = Toggle + " 00";
        static string DeleteCommandHelp = Delete+" 00";
        static string RollbackCommandHelp = Rollback + " 00";

        public static readonly string[] Help = new string[]
        {
            String.Format("{0,-30}creating new note",AddCommandHelp),
            String.Format("{0,-30}edit note",EditCommandHelp),
            String.Format("{0,-30}toggle progress flag note",ToggleCommandHelp),
            String.Format("{0,-30}delete note", DeleteCommandHelp),
            String.Format("{0,-30}rollback commands",RollbackCommandHelp),
            String.Format("{0,-30}show all notes",Show),
            String.Format("{0,-30}exit",Exit)
        };
    }
}
