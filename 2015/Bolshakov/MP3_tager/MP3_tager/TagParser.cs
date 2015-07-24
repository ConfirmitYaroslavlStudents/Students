using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MP3_tager
{
    public class TagParser
    {
        public string Pattern { get { return _pattern; } set { _pattern = value + '*'; } }

        public TagParser(string pattern)
        {
            Pattern = pattern;
            fillTagDictionary();
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
                return null; //If this is not tag - error => return null

            while (fileNameId<_fileName.Length) 
            {
                if(Pattern[patternId]==_fileName[fileNameId]) //May be it first symbol after tag
                {
                    var frames = fileNameId == _fileName.Length - 1 ? new Dictionary<FrameType, string>() : recMeth(patternId, fileNameId);
                    if (frames != null) //If recusive method returned non-null dictionary its actualy end of tag
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

        private void fillTagDictionary()
        {
            _frameTags = new Dictionary<string, FrameType>
            {
                {"<al>", FrameType.Album},
                {"<ar>", FrameType.Artist},
                {"<ti>", FrameType.Title},
                {"<tr>", FrameType.Track},
                {"<ye>", FrameType.Year}
            };
        }

        private Dictionary<string,FrameType> _frameTags;

        private string _fileName;
        private string _pattern;
    }
}
