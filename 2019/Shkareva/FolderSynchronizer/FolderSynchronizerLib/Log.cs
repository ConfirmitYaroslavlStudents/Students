using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FolderSynchronizerLib
{
    public class Log
    {
        private string FormLog(SyncData syncData)
        {
            string log = "";
            const string summary = "summary";
            const string verbose = "verbose";
            const string silent = "silent";

            switch (syncData.LogFlag)
            {
                case summary:
                    log = FormSummaryLog(syncData);
                    break;
                case verbose:
                    log = FormVerboseLog(syncData);
                    break;
                case silent:
                    log = "Folders are synchronized.";
                    break;
                default:
                    break;
            }

            return log;
        }

        private string FormVerboseLog(SyncData syncData)
        {
            string log = "";

            foreach(KeyValuePair<string,string> pair in syncData.FilesToCopy)
            {
                log.Concat("File " + pair.Key + " has been copied to " + Path.GetDirectoryName(pair.Value));
                log.Concat(Environment.NewLine);
            }

            if (syncData.NoDeleteFlag)
            {
                return log;
            }

            foreach(string path in syncData.FilesToDelete)
            {
                log.Concat("File " + path + " has been deleted");
                log.Concat(Environment.NewLine);
            }

            return log;
        }

        private string FormSummaryLog(SyncData syncData)
        {
            string log = "";

            log.Concat(syncData.FilesToCopy.Count + " files has been copied");
            log.Concat(Environment.NewLine);

            if (syncData.NoDeleteFlag)
            {
                return log;
            }

            log.Concat(syncData.FilesToDelete + " files has been deleted");
            return log;
        }

        public void Print(SyncData syncData)
        {
            Console.WriteLine(FormLog(syncData));
        }
    }
}
