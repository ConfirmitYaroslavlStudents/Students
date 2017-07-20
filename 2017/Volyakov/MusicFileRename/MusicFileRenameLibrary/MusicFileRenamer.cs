using System;
using System.IO;

namespace MusicFileRenameLibrary
{
    public class MusicFileRenamer
    {
        private string[] _extensions = new string[] { ".mp3", ".mp4" };
        private Parser _parser = new Parser();
        private FileWorker _fileWorker = new FileWorker();
        private IRenamer _renamer;
        
        public enum RenameType
        {
            ToFileName,
            ToTag
        }

        public void RenameMusicFiles(string directory,string searchPattern, bool recursive, RenameType renameType)
        {
            if (!IsCorrectExtension(searchPattern))
                throw new ArgumentException("Маска файла содержит неподдерживаемый формат файла");

            var filesForRename = _fileWorker.GetFiles(directory, searchPattern, recursive);

            _renamer = AppointRenamer(renameType);

            foreach(var file in filesForRename)
            {
                var parsedFile = _parser.ParseFile(file);
                _fileWorker.GetFileTags(parsedFile);
                _renamer.Rename(parsedFile);
                _parser.CollectFilePath(parsedFile);
                _fileWorker.SaveFile(file, parsedFile);
            }
        }

        private bool IsCorrectExtension(string searchPattern)
        {
            var currentExtension = _parser.GetFileExtension(searchPattern);
            for (int i = 0; i < _extensions.Length; i++)
            {
                if (_extensions[i] == currentExtension)
                    return true;
            }
            return false;
        }

        private IRenamer AppointRenamer(RenameType renameType)
        {
            switch(renameType)
            {
                case RenameType.ToFileName : return new FileNameRenamer();
                case RenameType.ToTag : return new TagRenamer();
                default: throw new ArgumentException("Неверно задан тип переименования");
            }
        }
    }
}
