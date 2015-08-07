using System.Collections.Generic;

namespace Mp3Handler
{
    public enum FrameType
    {
        Artist,
        Title,
        Album,
        Track,
        Year
    }


    // TODO :: Why static?
    public static class Frame
    {
        static Frame()
        {
            _stringKeyDictionary = new Dictionary<string,FrameType>
            {
                {"<al>", FrameType.Album},
                {"<ar>", FrameType.Artist},
                {"<ti>", FrameType.Title},
                {"<tr>", FrameType.Track},
                {"<ye>", FrameType.Year}
            };

            _enumKeyDictionary = new Dictionary<FrameType, string>();

            foreach (var frame in _stringKeyDictionary)
            {
                _enumKeyDictionary.Add(frame.Value,frame.Key);
            }
        }

        public static Dictionary<string,FrameType> StringKeyDictionary
        {
            get { return _stringKeyDictionary; }
        }

        public static Dictionary<FrameType,string> EnumKeyDictionary
        {
            get { return _enumKeyDictionary; }
        }

        public static FrameType GetEnum(string type)
        {
            return _stringKeyDictionary[type];
        }

        public static string GetString(FrameType type)
        {
            return _enumKeyDictionary[type];
        }

        private static readonly Dictionary<FrameType, string> _enumKeyDictionary;
        private static readonly Dictionary<string, FrameType> _stringKeyDictionary;
    }
}
