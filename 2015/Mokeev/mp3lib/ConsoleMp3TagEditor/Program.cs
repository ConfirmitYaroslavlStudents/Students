using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mp3_lib;

namespace ConsoleMp3TagEditor
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			var mp3 = new Mp3File(Environment.CurrentDirectory + @"/Changer - The biggest in the world.mp3");
		}
	}
}
