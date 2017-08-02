namespace Tagger
{
    class Program
    {
        static void Main(/*string[] args*/)
        {
            var args = new[] {@"D:\testTagger", "*", "toName", "recursive"};

            var input = TaggerLib.ParseInput.Parse(args);
            TaggerLib.Tagger.ChangeFiles(input);
        }
    }
}
