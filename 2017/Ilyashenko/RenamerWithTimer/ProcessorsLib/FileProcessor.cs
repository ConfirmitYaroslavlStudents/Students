using System;

namespace ProcessorsLib
{
    public class FileProcessor
    {
        public void Process(bool countTime)
        {
            var timeCounter = new TimeCounter(countTime);
            var permissionChecker = new PermissionChecker();
            var fileRenamer = new FileRenamer();
            var sampleFile = new Mp3File("SampleFile.mp3");

            timeCounter.CountTime(() => permissionChecker.CheckPermissions(sampleFile, UserRole.Administrator), "Check Permissions for SampleFile.mp3");
            timeCounter.CountTime(() => fileRenamer.Rename(sampleFile), "Rename SampleFile.mp3");
            Console.WriteLine(timeCounter.TotalTime);
        }
    }
}
