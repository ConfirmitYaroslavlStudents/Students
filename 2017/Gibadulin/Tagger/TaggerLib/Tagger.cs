namespace TaggerLib
{
    public class Tagger
    {
        public static void ChangeFiles(string[] args)
        {
            var input = ParseInput.Parse(args);
            var files = Dir.GetFiles(input);
            var action = Acting.Act(input.Modifier);
            foreach (var file in files)
            {               
                action.Act(file);
                file.Save();
            }
        }
    }
}
