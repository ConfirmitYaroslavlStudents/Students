using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mp3TagLib.Sync;

namespace Mp3TagLib.Plan
{
    [Serializable]
    public class PlanItem
    {
        public string FilePath { get; private set; }

        public ISyncRule Rule { get; private set; }

        public Mask Mask { get; private set; }

        public string Message { get; private set; }

        public PlanItem(Mask mask, string path, ISyncRule rule, string message)
        {
            Mask = mask;
            FilePath = path;
            Rule = rule;
            Message = message;
        }
    }
}