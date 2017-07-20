using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicFileRenameLibrary
{
    public class TagRenamer : IRenamer
    {
        public void Rename(FileShell parsedFile)
        {
            parsedFile.TagArtist = parsedFile.Artist;
            parsedFile.TagTitle = parsedFile.Title;
        }
    }
}
