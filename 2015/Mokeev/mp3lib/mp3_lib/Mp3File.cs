using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mp3_lib
{
    public class Mp3File
    {
	    private byte[] _id3v1Data;
	    private byte[] _id3v1ExtendedData;

		public string Title { get; set; }
		public string Artist { get; set; }
		public string Album { get; set; }
		public ushort Year { get; set; }
		public string Comment { get; set; }
		private byte ZeroByte { get; set; }
		public ushort TrackId { get; set; }
		public ushort Genre { get; set; }

	    public Mp3File(string file)
	    {
			if(!File.Exists(file)) throw new FileNotFoundException("File not found", file);

			var fileData = new MemoryStream();

		    using (var fs = new FileStream(file, FileMode.Open))
		    {
				fs.CopyTo(fileData);
		    }

		    var fileDataArray = fileData.ToArray();


		    var data = fileDataArray.Reverse().Take(128).Reverse().ToArray();

		    var tagCode = data.Take(3).ToArray();
		    
			var t1 = Encoding.UTF8.GetString(tagCode);
			var t2 = Encoding.ASCII.GetString(tagCode);

		    if (Encoding.UTF8.GetString(tagCode) == "TAG" || Encoding.ASCII.GetString(tagCode) == "TAG")
			    _id3v1Data = data;
	    }
    }
}
