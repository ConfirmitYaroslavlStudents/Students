using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList
{
    public static class PerformerCommands
    {
        public static void Add(List<Note> notes, List<object> command)
        {
            var note = (string)command[1];

            notes.Add(new Note { Text = note, isCompletedFlag = false });
        }

        public static void Edit(List<Note> notes, List<object> command)
        {
            var index = (int)command[1];

            if (!ValidationNumberInCommands.IsIndexInRange(index, 0, notes.Count - 1))
                throw new WrongEnteredCommandException("Note with that index not found.");

            var note = (string)command[2];

            notes[index].Text = note;
        }

        public static void Toggle(List<Note> notes, List<object> command)
        {
            var index = (int)command[1];

            if (!ValidationNumberInCommands.IsIndexInRange(index, 0, notes.Count - 1))
                throw new WrongEnteredCommandException("Note with that index not found.");

            notes[index].isCompletedFlag = !notes[index].isCompletedFlag;
        }

        public static void Delete(List<Note> notes, List<object> command)
        {
            var index = (int)command[1];

            if (!ValidationNumberInCommands.IsIndexInRange(index, 0, notes.Count - 1))
                throw new WrongEnteredCommandException("Note with that index not found.");

            notes.RemoveAt(index);
        }
        public static void Rollback(List<Note> notes, Stack<List<object>> rollback, List<object> command)
        {
            int count = (int)command[1];

            if (!ValidationNumberInCommands.IsIndexInRange(count, 1, rollback.Count))
                throw new WrongEnteredCommandException("Wrong count of rollback steps");

            HandlingRollbackCommand.Rollback(notes, rollback, count);
        }
        public static void Show(List<Note> notes)
        {
            PrintingConsoleMenuToDo.PrintAllNotes(notes);
        }
    }
}
