using System;

namespace TimeMeasurer.Variant2
{
    public class Processor
    {
        public void Process()
        {
            Mp3File file = new Mp3File();
            TimeMeasurer timeMeasurer = new TimeMeasurer(true);
            Mp3Renamer renamer = new Mp3Renamer(timeMeasurer);
            PermissionsChecker checker = new PermissionsChecker(timeMeasurer);
            
            for (int i = 0; i < 100; i++)
            {
                if (checker.CheckPermission(file, UserRole.Journalist))
                {
                    renamer.Rename(file);
                }
            }

            Console.WriteLine(timeMeasurer.GetResults());
        }
    }
}