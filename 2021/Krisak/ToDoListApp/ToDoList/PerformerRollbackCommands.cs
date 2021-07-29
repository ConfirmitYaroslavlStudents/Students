using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList
{
    public static class PerformerRollbackCommands
    {
        public static void Add(List<Note> notes, List<object> command)
        {
            var index = (int)command[1];
            var note = (Note)command[2];

            notes.Insert(index, note);
        }

        public static void Edit(List<Note> notes, List<object> command)
        {
            var index = (int)command[1];
            var noteText = (string)command[2];

            notes[index].Text = noteText;
        }

        public static void Toggle(List<Note> notes, List<object> command)
        {
            var index = (int)command[1];

            notes[index].isCompletedFlag = !notes[index].isCompletedFlag;
        }

        public static void Delete(List<Note> notes)
        {
            notes.RemoveAt(notes.Count-1);
        }
    }
}