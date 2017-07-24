namespace ProcessorsLib
{
    public class FileRenamer
    {
        public void Rename(Mp3File file)
        {
            file.Move("NewFile.mp3");
        }
    }
}
