namespace TaggerLib
{
    internal class ToName: IActing
    {
        private string NewName(File file)
        {
            var newFileName = string.Join(", ", file.Performers) + " - " + file.Title + System.IO.Path.GetExtension(file.Path);
            return newFileName;
        }

        public void Act(File file)
        {
            file.Name = NewName(file);
        }
    }
}
