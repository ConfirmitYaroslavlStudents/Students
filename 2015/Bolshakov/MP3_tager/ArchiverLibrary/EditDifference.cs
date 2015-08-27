using System.Collections.Generic;
using Mp3Handler;

namespace ArchiverLibrary
{
    public class EditDifference
    {
        public string OldName { get; set; }
        public string NewName { get; set; }
        public Dictionary<FrameType, string> OldTags { get; set; }
        public Dictionary<FrameType, string> NewTags { get; set; }

        //public static EditDifference operator+(EditDifference older, EditDifference newer)
        //{
            
        //}
    }
}