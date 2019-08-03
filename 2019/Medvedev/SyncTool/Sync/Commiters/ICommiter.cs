using System.Collections.Generic;
using Sync.Resolutions;

namespace Sync.Commiters
{
    public interface ICommiter
    {
        void Commit(IEnumerable<IResolution> resolutions);
    }
}