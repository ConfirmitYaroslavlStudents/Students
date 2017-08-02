namespace TaggerLib
{
    public class Tagger
    {
        public static void ChangeFiles(InputData input)
        {
            var files = Dir.GetFiles(input);
            var changer = ChangingFile.GetChange(input.Modifier);

            Action act = new FileChanger(changer);
            act = new TimeMeasurer(act);
            act = new CheckPermission(act);

            foreach (var file in files)
            {
                changer.FileForChange = file;
             
                act.Act();

                file.Save();
            }
        }
    }
}
