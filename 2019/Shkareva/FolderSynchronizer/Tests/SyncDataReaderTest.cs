using Xunit;
using FolderSynchronizerLib;
using System.Collections.Generic;
using System.Collections;

namespace Tests
{
    public class SyncDataReaderTest
    {

        [Theory]
        [ClassData(typeof(FolderSetSyncData))]
        public void CreateInputValidArgs(FolderSet set, SyncData expected)
        {
            var syncData = new SyncDataReader().Load(set);

            Assert.Equal(expected, syncData);
        }

    }

    public class FolderSetSyncData : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]> { };
        /*{
        new object[] { new FolderSet(new Folder("n:\\", new List<FileDescriptor>()),
          new Folder("l:\\", new List<FileDescriptor>()) ,
           new Folder("n:\\", new List<FileDescriptor>() {new FileDescriptor("1.pdf",1) }),
            new Folder("l:\\", new List<FileDescriptor>() {new FileDescriptor("2.txt", 5) }),
            false, "summary"), new SyncData(new Dictionary<string, string>(){ { "n:\\1.pdf" , "l:\\1.pdf" }, { "l:\\2.txt", "n:\\2.txt" } },
            new Dictionary<string, string>(), new List<string>())},

        new object[] { new FolderSet(new Folder("n:\\", new List<FileDescriptor>()),
          new Folder("l:\\", new List<FileDescriptor>()) ,
           new Folder("n:\\", new List<FileDescriptor>() {new FileDescriptor("1.pdf",1) }),
            new Folder("l:\\", new List<FileDescriptor>() {new FileDescriptor("1.pdf", 5) }),
            false, "summary"), new SyncData(new Dictionary<string, string>(),
            new Dictionary<string, string>(){ { "n:\\1.pdf" , "l:\\1.pdf" }}, new List<string>())},

        new object[] { new FolderSet(new Folder("n:\\", new List<FileDescriptor>()),
          new Folder("l:\\", new List<FileDescriptor>()) ,
           new Folder("n:\\", new List<FileDescriptor>() {new FileDescriptor("1.pdf",1) }),
            new Folder("l:\\", new List<FileDescriptor>() {new FileDescriptor("2.txt", 5) }),
            false, "summary"), new SyncData(new Dictionary<string, string>(){ { "n:\\1.pdf" , "l:\\1.pdf" }, { "l:\\2.txt", "n:\\2.txt" } },
            new Dictionary<string, string>(), new List<string>())},

        new object[] { new FolderSet(new Folder("n:\\", new List<FileDescriptor>(){new FileDescriptor("1.pdf",1) }),
          new Folder("l:\\", new List<FileDescriptor>(){new FileDescriptor("1.pdf",1) }) ,
           new Folder("n:\\", new List<FileDescriptor>()),
            new Folder("l:\\", new List<FileDescriptor>() {new FileDescriptor("1.pdf",1)}),
            false, "summary"), new SyncData(new Dictionary<string, string>(),
            new Dictionary<string, string>(), new List<string>(){  "l:\\1.pdf"})},

        new object[] { new FolderSet(new Folder("n:\\", new List<FileDescriptor>(){new FileDescriptor("1.pdf",1), new FileDescriptor("2.jpg", 2) }),
          new Folder("l:\\", new List<FileDescriptor>(){new FileDescriptor("1.pdf",1), new FileDescriptor("2.jpg", 2) }) ,
           new Folder("n:\\", new List<FileDescriptor>(){ new FileDescriptor("2.jpg", 2)}),
            new Folder("l:\\", new List<FileDescriptor>() {new FileDescriptor("1.pdf",1)}),
            false, "summary"), new SyncData(new Dictionary<string, string>(),
            new Dictionary<string, string>(), new List<string>(){ "l:\\1.pdf","n:\\2.jpg" })}

        };*/

        public IEnumerator<object[]> GetEnumerator()
        { return _data.GetEnumerator(); }

        IEnumerator IEnumerable.GetEnumerator()
        { return GetEnumerator(); }
    }
}
