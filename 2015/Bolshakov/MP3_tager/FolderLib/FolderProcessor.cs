using System;
using System.Collections.Generic;
using Mp3Handler;

namespace FolderLib
{
    public class FolderProcessor
    {
        public FolderProcessor(IFolderHandler folderHandler, IFileHandler fileHandler)
        {
            _folderHandler = folderHandler;
            _lateFileHandler = new LateWriteFileHandler(fileHandler);
        }

        public FolderProcessor()
        {
            _folderHandler = new FolderHandler();
            FileHandlerBuilder();
        }

        public LateWriteFileHandler LateFileHandler
        {
            get { return _lateFileHandler; }
        }

        public Dictionary<string,Dictionary<FrameType,TagDifference>> GetDifferences(string path, string pattern)
        {
            var files = _folderHandler.GetAllMp3s(path);
            if (files.Count == 0)
                return null;
            var mp3Processor = new Mp3FileProcessor(_lateFileHandler);
            var differences = new Dictionary<string, Dictionary<FrameType, TagDifference>>();
            foreach (var file in files)
            {
                _lateFileHandler.FilePath = file;
                var diff = mp3Processor.Difference(pattern);
                if(diff != null)
                    differences.Add(file,diff);
            }
            return differences;
        }

        public bool PrepareSynch(string path, string pattern)
        {
            if (!_synchInProgres)
            {
                var mp3Processor = new Mp3FileProcessor(_lateFileHandler);

                var files = _folderHandler.GetAllMp3s(path);
                if (files.Count == 0)
                    return false;
                _synchInProgres = true;

                foreach (var file in files)
                {
                    _lateFileHandler.FilePath = file;
                    mp3Processor.Synchronize(pattern);
                }
                return true;
            }
            return false;
        }

        public bool CompleteSych(int[] notToWrite)
        {
            if (_synchInProgres)
            {
                _synchInProgres = false;
                _lateFileHandler.WriteFiles(notToWrite);
                _lateFileHandler = null;
                return true;
            }
            return false;
        }


        private void FileHandlerBuilder()
        {
            var fileHandler = new Id3LibFileHandler();
            _lateFileHandler = new LateWriteFileHandler(fileHandler);
        }

        private LateWriteFileHandler _lateFileHandler;
        private IFolderHandler _folderHandler;
        private bool _synchInProgres;
    }
}
