using Sync.Resolutions;

namespace Sync.ResolvingPolicies
{
    public interface IResolvingPolicy
    {
        IResolution Resolve(Conflict conflict);
    }
}