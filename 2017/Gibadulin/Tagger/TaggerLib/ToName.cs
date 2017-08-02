using System;

namespace TaggerLib
{
    public class ToName : IChangingFile
    {
        public File FileForChange { get; set; }

        public void Change()
        {
            if (FileForChange == null)
                throw new ArgumentNullException();

            FileForChange.Name = NewName();
        }

        private string NewName()
        {
            var newFileName = string.Join(", ", FileForChange.Performers) + " - " + FileForChange.Title +
                              System.IO.Path.GetExtension(FileForChange.Path);
            return newFileName;
        }
    }
}