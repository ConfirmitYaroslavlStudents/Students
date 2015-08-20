using System;
using System.Linq;

namespace Mp3TagLib.Sync
{
    public class SyncMethods
    {
        public bool TryChangeName(Mask mask,Tager tager)
        {
            try
            {
                tager.ChangeName(mask);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool TryChangeTags(Mask mask, Tager tager)
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
