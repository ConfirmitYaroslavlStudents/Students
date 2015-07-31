using System.Collections.Generic;
using System.Text;

namespace Mp3Handler
{
    public class TagParser
    {
        public string Pattern { get { return _pattern; } set { _pattern = value + '*'; } }

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value + '*'; }
        }

        public TagParser(string pattern)
        {
            Pattern = pattern;
        }

        public Dictionary<FrameType,string> GetFrames(string fileName)
        {
            FileName = fileName;
            
            return DeterminateNextTag(0, 0);
        }

        private Dictionary<FrameType,string> DeterminateNextTag(int patternId,int fileNameId)
        {
            var tag = new StringBuilder();
            var value = new StringBuilder();

            while(IndexesAreValid(patternId,fileNameId) && Pattern[patternId]==FileName[fileNameId]) //Go to first different symbol
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
                    var frames = fileNameId == _fileName.Length - 1 ? new Dictionary<FrameType, string>() : DeterminateNextTag(patternId, fileNameId);
                    if (frames != null) //If recusive method returned non-null dictionary its actualy end of tag
                    {
                        frames.Add(Frame.GetEnum(tag.ToString()), value.ToString());
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

        private string _fileName;

        
        private string _pattern;
    }
}
