﻿using System.Collections.Generic;
using System.Linq;
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

        public Dictionary<FrameType,string> GetTagsValue(string fileName)
        {
            FileName = fileName;

            Dictionary<FrameType, string> frames;
            DeterminateNextTag(new StrWithCursor(FileName), new StrWithCursor(Pattern), out frames);

            return frames;
        }

        public List<FrameType> GetTags()
        {
            var pattern = new StrWithCursor(Pattern);
            var parsedTags = new SortedSet<FrameType>();
            var tag = new StringBuilder();

            while (pattern!='*')
            {
                if (pattern == '<')
                {
                    DeterminateTag(pattern, tag);
                    parsedTags.Add(Frame.GetEnum(tag.ToString()));
                    tag.Clear();
                }
                else
                    pattern++;
            }

            return parsedTags.Count != 0 ? parsedTags.ToList() : null;
        }

        private void DeterminateNextTag(StrWithCursor fileName, StrWithCursor pattern, out Dictionary<FrameType, string> frames)
        {
            //TODO ::  Why do we need to use a copy constructor?
            fileName = new StrWithCursor(fileName);
            pattern = new StrWithCursor(pattern);

            var tag = new StringBuilder(); //Detreminated tag
            var value = new StringBuilder(); //Determinated value

            while(fileName != '*' && pattern != '*' && fileName == pattern) //Go to first different symbol
            {
                fileName++;
                pattern++;
            }

            if (pattern == '<')
                DeterminateTag(pattern, tag);
            else
            {
                frames = null;
                return; //If this is not tag - error => return null
            }

            while (fileName!='*' || pattern=='*')
            {
                if(pattern==fileName) //Maybe it first symbol after tag
                {
                    if (fileName == '*') //if end of string than this valid string
                    {
                        frames = new Dictionary<FrameType, string> {{Frame.GetEnum(tag.ToString()), value.ToString()}}; //Create and eturn them
                        return;
                    }

                    DeterminateNextTag(fileName,pattern, out frames); //If not end of string, determinate next
                    if (frames != null) //If recusive method returned non-null dictionary its actualy end of tag
                    {
                        frames.Add(Frame.GetEnum(tag.ToString()), value.ToString()); //We should return them
                        return;
                    } //If returned null, then this not end of string. Continue reading
                }
                value.Append(fileName.Value); 
                fileName++;
            }

            frames = null;
        }

        private void DeterminateTag(StrWithCursor pattern, StringBuilder tag)
        {
            do
            {
                tag.Append(pattern.Value);
                pattern++;
            }
            while (pattern != '>' && pattern != '*');
            tag.Append(pattern.Value);
            pattern++;
        }

        private string _fileName;

        private string _pattern;
    }
}
