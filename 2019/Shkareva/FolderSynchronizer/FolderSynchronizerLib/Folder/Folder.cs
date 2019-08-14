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
        public List<Item> FilesList;
       
        public Folder(string address)
        {
            Path = address;            
            FilesList = new List<Item>();
        }

        public Folder() { }
    }
}
