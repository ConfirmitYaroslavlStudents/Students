using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Mp3TagLib.Sync;

namespace Mp3TagLib.Plan
{
    internal class PlanBuilder
    {
        private Tager _tager;
        private Mask _mask;

        public Dictionary<string, string> ErrorFiles { get; private set; }


        public PlanBuilder(Tager tager, Mask mask)
        {
            _tager = tager;
            _mask = mask;
            ErrorFiles = new Dictionary<string, string>();
        }

        internal SyncPlan Build(IEnumerable<IMp3File> files)
        {
            var plan = new SyncPlan();

            foreach (var mp3File in files)
            {
                _tager.CurrentFile = mp3File;
                var fileProblems = _tager.GetFileProblems(_mask);

                if (fileProblems.Any())
                {
                    if (fileProblems.Count > 1)
                    {
                        ErrorFiles.Add(mp3File.Name, "bad name and tags");

                        continue;
                    }

                    switch (fileProblems.First())
                    {
                        case FileProblem.BadName:
                            plan.Add(new PlanItem(_mask, mp3File.Path, new DefaultSyncRule(),
                                _tager.CurrentFile.Name + " rename to " + _tager.GenerateName(_mask)));
                            break;
                        case FileProblem.BadTags:
                            plan.Add(new PlanItem(_mask, mp3File.Path, new DefaultSyncRule(), GetBadTagsMessage()));
                            break;

                    }
                }
                else
                {
                    plan.Add(new PlanItem(_mask, mp3File.Path, new DefaultSyncRule(),
                                _tager.CurrentFile.Name + " rename to " + _tager.GenerateName(_mask)));
                }


            }
            return plan;
        }

        private string GetBadTagsMessage()
        {
            StringBuilder builder = new StringBuilder();
            var badTags = _tager.GetIncorectTags(_mask);
            builder.AppendLine("Tags change in " + _tager.CurrentFile.Name + ":");
            var tagsFromName = _tager.GetTagsFromName(_mask);
            foreach (var badTag in (from tag in badTags where _mask.Contains(tag.ToString().ToLower()) select tag))
            {
                builder.AppendLine("\t" + badTag + " is empty." + "New value is " + "\"" + tagsFromName.GetTag(badTag.ToString()) + "\"");
            }
            return builder.ToString();
        }
    }
}
