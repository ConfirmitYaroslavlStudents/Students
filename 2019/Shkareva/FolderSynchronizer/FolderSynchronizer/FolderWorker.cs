using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;

namespace FolderSynchronizer
{
    public class FolderWorker
    {
        public Folder LoadSerializedFolder(string path)
        {
            string filename = GetFileName(path);

            if (!File.Exists(filename))
            {
                return new Folder(path);
            }

            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Folder));

            using (FileStream fileStream = new FileStream(filename, FileMode.Open))
            {
               Folder folder = (Folder)jsonFormatter.ReadObject(fileStream);
               return folder;                                
            }
        }

        public Folder LoadFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException();
            }

            var filesPathList = new List<string>();
            filesPathList.AddRange(Directory.GetFiles(path));
            var internalFoldersPaths = Directory.GetDirectories(path).ToList();
            int countPath = 0;

            while (countPath < internalFoldersPaths.Count)
            {
                var _path = internalFoldersPaths[countPath];
                filesPathList.AddRange(Directory.GetFiles(_path));
                internalFoldersPaths.AddRange(Directory.GetDirectories(_path));
                countPath++;
            }

            var folder = new Folder(path);
            folder.FilesPathList = filesPathList;
            
            return folder;
        }

        public void SerializeFolder(string path)
        {
            Folder folder = LoadFolder(path);
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Folder));
            string filename = GetFileName(path);

            if (File.Exists(filename))
            {
                File.Delete(filename);
            }

            using (FileStream fileStream = new FileStream(filename, FileMode.Create))
            {
                jsonFormatter.WriteObject(fileStream, folder);
            }            
        }

        private static string GetFileName(string path)
        {
            string filename = path.Replace("\\", ".");
            filename = filename.Replace(":", "") + ".json";
            return filename;
        }
    }
}
