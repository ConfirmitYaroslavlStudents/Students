

namespace HomeWork3
{
    public class MP3Renamer : IRenamer
    {
        public void Rename(MP3File file)
        {
            file.FileName = "New" + file.FileName;
        }
    }
}
