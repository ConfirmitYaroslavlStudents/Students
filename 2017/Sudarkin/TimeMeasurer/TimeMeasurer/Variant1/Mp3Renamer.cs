namespace TimeMeasurer.Variant1
{
    public class Mp3Renamer
    {
        public void Rename(Mp3File file)
        {
            file.MoveTo("NewName");
        }
    }
}