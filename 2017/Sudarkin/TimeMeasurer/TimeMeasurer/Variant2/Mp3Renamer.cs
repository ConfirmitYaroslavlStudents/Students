namespace TimeMeasurer.Variant2
{
    public class Mp3Renamer
    {
        private readonly TimeMeasurer _timeMeasurer;

        public Mp3Renamer(TimeMeasurer timeMeasurer)
        {
            _timeMeasurer = timeMeasurer;
        }

        public void Rename(Mp3File file)
        {
            _timeMeasurer.Measure(() => 
            {
                file.MoveTo("NewName");
            }, "Mp3Renamer::Rename");
        }
    }
}