using System;

namespace TaggerLib
{
    internal class ToTag : IActing
    {
        private static string[] SplitName(string name)
        {
            var tags = name.Split(new[] {" - "}, StringSplitOptions.RemoveEmptyEntries);
            return tags;
        }

        private string[] NewPerformers(File file)
        {
            var tags = SplitName(file.Name);
            var newPerformes = tags[0].Split(new[] {", "}, StringSplitOptions.RemoveEmptyEntries);

            return newPerformes;
        }

        private string NewTitle(File file)
        {
            var tags = SplitName(file.Name);
            var newTitle = tags[1];

            return newTitle;
        }

        public void Act(File file)
        {
            file.Performers = NewPerformers(file);
            file.Title = NewTitle(file);
        }
    }
}
