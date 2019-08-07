using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FolderSynchronizerLib
{
    [DataContract]
    public class Folder
    {
        [DataMember]
        public string Path;
        
        [DataMember]
        public List<string> FilesPathList;
       
        public Folder(string address)
        {
            Path = address;            
            FilesPathList = new List<string>();
        }

        public Folder() { }
    }
}
