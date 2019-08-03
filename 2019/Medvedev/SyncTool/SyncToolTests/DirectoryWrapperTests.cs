using Sync.Wrappers;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sync.Tests
{
    public class DirectoryWrapperTests
    {
        [Fact]
        public void CreateDirectory_CreatesDirectory()
        {
            var directory = new DirectoryWrapper("master");
            var subDirectory = directory.CreateDirectory("a");

            Assert.Contains(subDirectory, directory.EnumerateDirectories());
        }

        [Fact]
        public void CreateDirectoryWithEmptyName_ThrowsArgumentException()
        {
            var directory = new DirectoryWrapper("master");
            Action action = () => directory.CreateDirectory("");

            Assert.Throws<ArgumentException>(action);
        }

        [Fact]
        public void CreateDirectoryWithNull_ThrowsArgumentException()
        {
            var directory = new DirectoryWrapper("master");
            Action action = () => directory.CreateDirectory(null);

            Assert.Throws<ArgumentException>(action);
        }

        [Fact]
        public void CreateExistingDirectory_NoExtraDirectory()
        {
            var directory = new DirectoryWrapper("master");
            directory.CreateDirectory("a");
            directory.CreateDirectory("a");

            Assert.Single(directory.EnumerateDirectories());
        }

        [Fact]
        public void CreateFile_CreatesFile()
        {
            var directory = new DirectoryWrapper("master");
            var file = directory.CreateFile("a", new FileAttributes(1, DateTime.MaxValue));

            Assert.Contains(file, directory.EnumerateFiles());
        }

        [Fact]
        public void CreateFileWithEmptyName_ThrowsArgumentException()
        {
            var directory = new DirectoryWrapper("master");
            Action action = () => directory.CreateFile("", new FileAttributes(1, DateTime.MaxValue));

            Assert.Throws<ArgumentException>(action);
        }

        [Fact]
        public void CreateFileWithNull_ThrowsArgumentException()
        {
            var directory = new DirectoryWrapper("master");
            Action action = () => directory.CreateFile(null, new FileAttributes(1, DateTime.MaxValue));

            Assert.Throws<ArgumentException>(action);
        }

        [Fact]
        public void EnumerateDirectories_EnumeratesAllSubDirectories()
        {
            var dir = new DirectoryWrapper("master");
            dir.CreateDirectory("a");
            dir.CreateDirectory("b").CreateDirectory("c");

            var expected = new HashSet<string>(new string[]{"a", "b"});
            var actual = dir.EnumerateDirectories().Select(x => x.Name).ToHashSet();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void EnumerateFiles_EnumeratesAllFiles()
        {
            var dir = new DirectoryWrapper("master");
            dir.CreateFile("a", new FileAttributes(1, DateTime.MaxValue));
            dir.CreateFile("b", new FileAttributes(2, DateTime.MinValue));

            var expected = new HashSet<string>(new string[] { "a", "b" });
            var actual = dir.EnumerateFiles().Select(x => x.Name).ToHashSet();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void EnumerateContainment_EnumeratesDirectoryContainment()
        {
            var dir = new DirectoryWrapper("master");
            dir.CreateFile("a", new FileAttributes(1, DateTime.MaxValue));
            dir.CreateFile("b", new FileAttributes(2, DateTime.MinValue));
            dir.CreateDirectory("c");

            var expected = new HashSet<string>(new string[] { "a", "b", "c" });
            var actual = dir.EnumerateContainment().Select(x => x.Name).ToHashSet();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Equal_EqualDirecotories_IgnoreCase_True()
        {
            var dir1 = new DirectoryWrapper("master");
            var dir2 = new DirectoryWrapper("MaStEr");

            Assert.True(dir1.Equals(dir2));
        }
    }
}