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
        public List<FileDescriptor> FilesList;
       
        public Folder(string address)
        {
            Path = address;            
            FilesList = new List<FileDescriptor>();
        }

        public Folder(string path, List<FileDescriptor> fileDescriptors)
        {
            Path = path;
            FilesList = fileDescriptors;
        }

        public Folder() { }
    }
}
