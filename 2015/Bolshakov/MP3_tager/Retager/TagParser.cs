using System.Collections.Generic;
using System.Text;

namespace RetagerLib
{
    public class TagParser
    {
        public string Pattern { get { return _pattern; } set { _pattern = value + '*'; } }

        public TagParser(string pattern)
        {
            Pattern = pattern;
            FillTagDictionary();
        }

        public Dictionary<FrameType,string> GetFrames(string fileName)
        {
            _fileName = fileName+'*';
            
            return RecMeth(0, 0);
        }

        private Dictionary<FrameType,string> RecMeth(int patternId,int fileNameId)
        {
            var tag = new StringBuilder();
            var value = new StringBuilder();

            while(IndexesAreValid(patternId,fileNameId) && Pattern[patternId]==_fileName[fileNameId]) //Go to first different symbol
            {
                patternId++;
                fileNameId++;
            }

            if (Pattern[patternId] == '<') 
            {
                patternId = DeterminateTag(patternId, tag);
            }
            else
                return null; //If this is not tag - error => return null

            while (fileNameId<_fileName.Length) 
            {
                if(Pattern[patternId]==_fileName[fileNameId]) //May be it first symbol after tag
                {
                    var frames = fileNameId == _fileName.Length - 1 ? new Dictionary<FrameType, string>() : RecMeth(patternId, fileNameId);
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

        private int DeterminateTag(int patternId, StringBuilder tag)
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

        private bool IndexesAreValid(int patternIndex, int fileNameIndex)
        {
            if (patternIndex < Pattern.Length && fileNameIndex < _fileName.Length) return true;
            return false;
        }

        private void FillTagDictionary()
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
