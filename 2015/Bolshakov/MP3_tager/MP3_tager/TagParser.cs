using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MP3_tager
{
    class TagParser
    {
        public string Pattern { get; private set; }

        public TagParser(string pattern)
        {
            Pattern = pattern+'*';
            _frameTags = new Dictionary<string,FrameType>();
            _frameTags.Add("<al>",FrameType.Album);
            _frameTags.Add("<ar>",FrameType.Artist);
            _frameTags.Add("<ti>",FrameType.Title);
            _frameTags.Add("<tr>",FrameType.Track);
            _frameTags.Add("<ye>",FrameType.Year);
        }

        public Dictionary<FrameType,string> GetFrames(string fileName)
        {
            _fileName = fileName+'*';
            var frames = new Dictionary<FrameType, string>();
            
            return recMeth(0, 0);
        }

        private Dictionary<FrameType,string> recMeth(int patternId,int fileNameId)
        {
            var tag = new StringBuilder();
            var value = new StringBuilder();

            while(indexesAreValid(patternId,fileNameId) && Pattern[patternId]==_fileName[fileNameId]) //Go to first different symbol
            {
                patternId++;
                fileNameId++;
            }

            if (Pattern[patternId] == '<')
            {
                patternId = determinateTag(patternId, tag);
            }
            else
                return null;

            while (fileNameId<_fileName.Length)
            {
                if(Pattern[patternId]==_fileName[fileNameId])
                {
                    var frames = fileNameId == _fileName.Length - 1 ? new Dictionary<FrameType, string>() : recMeth(patternId, fileNameId);
                    if (frames != null)
                    {
                        frames.Add(_frameTags[tag.ToString()], value.ToString());
                        return frames;
                    }
                }
                value.Append(_fileName[fileNameId]);
                fileNameId++;
            }

            return null;
        }

        private int determinateTag(int patternId, StringBuilder tag)
        {
            do
            {
                tag.Append(Pattern[patternId]);
                patternId++;
            }
            while (patternId < Pattern.Length && Pattern[patternId] != '>');
            tag.Append(Pattern[patternId]);
            patternId++;
            return patternId;
        }

        private bool indexesAreValid(int patternIndex, int fileNameIndex)
        {
            return patternIndex < Pattern.Length && fileNameIndex < _fileName.Length;
        }

        private bool indexesInEnd(int patternIndex, int fileNameIndex)
        {
            return patternIndex == Pattern.Length-1 && fileNameIndex == _fileName.Length-1;
        }

        private Dictionary<string,FrameType> _frameTags;

        private string _fileName;
    }
}
