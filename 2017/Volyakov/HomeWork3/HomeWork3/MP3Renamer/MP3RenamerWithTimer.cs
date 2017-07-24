
namespace HomeWork3
{
    public class MP3RenamerWithTimer:Timer, IMP3Renamer
    {
        private MP3Renamer _renamer = new MP3Renamer();
        
        public void Rename(MP3File file)
        {
            timer.Start();
            _renamer.Rename(file);
            timer.Stop();
            time += timer.ElapsedMilliseconds;
        }
    }
}
