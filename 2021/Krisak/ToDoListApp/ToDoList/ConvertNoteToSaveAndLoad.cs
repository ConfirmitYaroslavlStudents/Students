using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ToDoList
{
    public static class ConvertNoteToSaveAndLoad
    {
        static string _stringWhenFlagTrue = "=ON";
        static string _stringWhenFlagFalse = "=OFF";

        public static IEnumerable<Note> ConvertLinesAfterLoading(IEnumerable<string> lines)
        {
            var notes = new List<Note>();

            foreach (var line in lines)
                notes.Add(LineToNote(line));

            return notes;
        }

        public static IEnumerable<string> ConvertNoteToSave(IEnumerable<Note> notes)
        {
            var lines = new List<string>();

            foreach (var note in notes)
                lines.Add(NoteToLine(note));

            return lines;
        }

        static Note LineToNote(string line)
        {
            var regex = new Regex(String.Format("{0}$|{1}$", _stringWhenFlagTrue, _stringWhenFlagFalse));

            var flag = regex.Match(line).Value.ToString();
            var textNote = regex.Split(line)[0];

            if (flag == _stringWhenFlagTrue)
                return new Note { Text = textNote, isCompletedFlag = true };

            if (flag == _stringWhenFlagFalse)
                return new Note { Text = textNote, isCompletedFlag = false };

            return new Note { Text = line, isCompletedFlag = false };
        }

        static string NoteToLine(Note note)
        {
            if (note.isCompletedFlag)
                return note.Text + _stringWhenFlagTrue;

            return note.Text + _stringWhenFlagFalse;
        }
    }
}
