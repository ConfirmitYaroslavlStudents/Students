using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mp3TagLib.Plan
{
    public interface IPlanProvider
    {
        SyncPlan Load(string path);
        void Save(SyncPlan plan, string path);
    }
}
