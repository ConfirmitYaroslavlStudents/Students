using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicFileRenameLibrary
{
    public class FileNameRenamer : IRenamer
    {
        public void Rename(FileShell parsedFile)
        {
            parsedFile.Artist = parsedFile.TagArtist;
            parsedFile.Title = parsedFile.TagTitle;
        }
    }
}
