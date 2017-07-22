using System;

namespace TimeMeasurer.Variant1
{
    public class Processor
    {
        public void Process()
        {
            Mp3File file = new Mp3File();
            TimeMeasurer timeMeasurer = new TimeMeasurer(true);
            Mp3Renamer renamer = new Mp3Renamer();
            PermissionsChecker checker = new PermissionsChecker();

            for (int i = 0; i < 100; i++)
            {
                if(timeMeasurer.Measure(
                    () => checker.CheckPermission(file, UserRole.Journalist), "CheckPermission"))
                {
                    timeMeasurer.Measure(
                        () => renamer.Rename(file), "Rename");
                }
            }

            Console.WriteLine(timeMeasurer.GetResults());
        }
    }
}