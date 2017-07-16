using System;
using System.IO;
using System.Linq;

namespace Rename
{
    public class FileRenamer
    {
        private string _baseDirectory;

        public FileRenamer(string dir)
        {
            _baseDirectory = dir;
        }

        public void Rename(string[] args)
        {
            if (args.Length < 2 || args.Length > 3)
            {
                throw new ArgumentException();
            }
            try
            {
                var dir = new DirectoryInfo(_baseDirectory);
            }
            catch (Exception exception)
            {
                throw exception;
            }

            var pattern = args[0];
            bool recursive = false;
            int next = 1;
            if (args[next] == "-recursive")
            {
                recursive = true;
                next++;
            }
            if (args[next] == "-toFileName")
            {
                MakeFileNames(GetFilePaths(_baseDirectory, pattern, recursive));
            }
            else if (args[next] == "-toTag")
            {
                MakeFileTags(GetFilePaths(_baseDirectory, pattern, recursive));
            }
            else
            {
                throw new ArgumentException();
            }
        }
        
        public string[] GetFilePaths(string currentDirectory, string pattern, bool recursive)
        {
            return recursive ? Directory.GetFiles(currentDirectory, pattern, SearchOption.AllDirectories) : Directory.GetFiles(currentDirectory, pattern);
        }

        public void MakeFileNames(string[] filePaths)
        {
            foreach (var fPath in filePaths)
            {
                var currFile = TagLib.File.Create(fPath);
                var artists = new string[] { currFile.Tag.AlbumArtists[0], currFile.Tag.FirstAlbumArtist, currFile.Tag.Artists[0], currFile.Tag.FirstArtist, currFile.Tag.Performers[0], currFile.Tag.FirstPerformer, currFile.Tag.JoinedArtists, currFile.Tag.JoinedPerformers, currFile.Tag.JoinedAlbumArtists };
                var artist = (from _artist in artists where !_artist.Equals(String.Empty) select _artist).First();
                var newPath = Path.GetDirectoryName(fPath) + "\\" + artist + " - " + currFile.Tag.Title + Path.GetExtension(fPath);
                if (!File.Exists(newPath))
                    File.Move(fPath, newPath);
            }
        }

        public void MakeFileTags(string[] filePaths)
        {
            foreach (var fPath in filePaths)
            {
                var fileName = Path.GetFileName(fPath);
                string artist = String.Empty;
                string title = String.Empty;

                int i = 0;
                while (i + 1 < fileName.Length && fileName[i + 1] != '-')
                {
                    artist += fileName[i];
                    i++;
                }
                artist = artist.Substring(0, artist.Length);
                title = fileName.Substring(i + 3, fileName.Length - (i + 3) - Path.GetExtension(fPath).Length);
                var currFile = TagLib.File.Create(fPath);
                currFile.Tag.Performers = new string[] { artist };
                currFile.Tag.Title = title;
                currFile.Save();
            }
        }
    }
}
