namespace Mp3Handler
{
    public class TagDifference
    {
        public TagDifference(string fileValue,string pathValue)
        {
            FileValue = fileValue;
            PathValue = pathValue;
        }

        public string FileValue { get; set; }
        public string PathValue { get; set; }
    }
}
