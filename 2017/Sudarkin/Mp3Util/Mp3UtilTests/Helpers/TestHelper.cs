using System.IO;
using System.IO.Abstractions.TestingHelpers;
using Mp3UtilConsole;
using TagFile = TagLib.File;

namespace Mp3UtilTests.Helpers
{
    public class TestHelper
    {
        public static byte[] GetDummyMp3()
        {
            return File.ReadAllBytes($"{GetTestRootDirectory()}/Dummy.mp3");
        }

        public static string GetTestRootDirectory()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
            return directoryInfo.Parent.Parent.FullName;
        }

        public static Mp3File GetMp3File(string filename, MockFileSystem fileSystem)
        {
            MockFileStream mockStream = new MockFileStream(fileSystem, filename);
            TagFile tagFile = TagFile.Create(new FileAbstraction(filename, mockStream));
            return new Mp3File(tagFile);
        }
    }
}