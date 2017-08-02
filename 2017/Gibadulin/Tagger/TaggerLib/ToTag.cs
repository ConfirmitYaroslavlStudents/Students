using System;

namespace TaggerLib
{
    public class ToTag : IChangingFile
    {
        public File FileForChange { get; set; }

        public void Change()
        {
            if (FileForChange == null)
                throw new ArgumentNullException();

            FileForChange.Performers = NewPerformers();
            FileForChange.Title = NewTitle();
        }

        private string[] SplitName()
        {
            var name = FileForChange.Name;
            var tags = name.Split(new[] {" - "}, StringSplitOptions.RemoveEmptyEntries);
            return tags;
        }

        private string[] NewPerformers()
        {
            var tags = SplitName();
            var newPerformes = tags[0].Split(new[] {", "}, StringSplitOptions.RemoveEmptyEntries);

            return newPerformes;
        }

        private string NewTitle()
        {
            var tags = SplitName();
            var newTitle = tags[1];

            return newTitle;
        }
    }
}