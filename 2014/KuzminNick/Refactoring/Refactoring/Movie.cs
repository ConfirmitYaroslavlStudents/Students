using System.Runtime.Serialization;

namespace VideoService
{
    [DataContract]
    public class Movie
    {
        public Movie(string title)
        {
            Title = title;
        }

        [DataMember]
        public string Title
        {
            get;
            set;
        }
    }
}
