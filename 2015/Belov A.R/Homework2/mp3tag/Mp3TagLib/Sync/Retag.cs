using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mp3TagLib.Sync
{
    class Retag:SyncOperation
    {
        public override bool Call(Mask mask, Tager tager)
        {
            try
            {
                var tags = new Mp3Tags();
                var tagsFromName = mask.GetTagValuesFromString(tager.CurrentFile.Name);
                foreach (var tag in tagsFromName.First())
                {
                    tags.SetTag(tag.Key, tag.Value);
                }
                tager.ChangeTags(tags);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
