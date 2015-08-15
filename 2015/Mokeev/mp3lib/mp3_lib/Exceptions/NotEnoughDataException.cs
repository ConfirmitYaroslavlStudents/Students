using System;
using System.Collections.Generic;

namespace mp3lib.Exceptions
{
	class NotEnoughDataException : DataExctracterException
	{
		public Dictionary<TagType, string> Data { get; set; }

		public NotEnoughDataException(string message, IEnumerable<TagType> emptyTags, Dictionary<TagType, string> data) : base(message)
		{
			Data = data;
			foreach (var tag in emptyTags)
			{
				Data.Add(tag, string.Empty);
			}
		}
	}
}