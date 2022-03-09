using NUnit.Framework;
using FileCompare;
using System.IO.Abstractions.TestingHelpers;
using System.Collections.Generic;
using System.IO.Abstractions;
using System;
using System.IO;
using System.Linq;

namespace FileCompare.Test
{
    [TestFixture]
    public class FileCompare_Test
    {
        public string TestRootDirectory => "TestDirectory";
        public string TestDir1 => TestRootDirectory + "\\TestDir1";
        public string TestDir2 => TestRootDirectory + "\\TestDir2";
        
        public string TestDbPath => TestRootDirectory + "\\TestDb.db";

        public IEnumerable<string> FilesDir1 { get; }
            = new[]
            {
                @"TestDir1\berlin - Copy.mp4",
                @"TestDir1\berlin.mp4",
                @"TestDir1\lake.jpg",
                @"TestDir1\mountain.jpg",
                @"TestDir1\park.jpg",
                @"TestDir1\SubDir1\lake.jpg",
                @"TestDir1\SubDir1\plaque.jpg",
                @"TestDir1\SubDir1\SubDir2\moon.jpg",
                @"TestDir1\SubDir1\SubDir2\mountain-differentName.jpg"
            };

        public IEnumerable<string> ExpectedAddedDir1 { get; }
            = new[]
            {
                @"TestDir1\berlin.mp4",
                @"TestDir1\lake.jpg",
                @"TestDir1\mountain.jpg",
                @"TestDir1\park.jpg",
                @"TestDir1\SubDir1\plaque.jpg",
                @"TestDir1\SubDir1\SubDir2\moon.jpg",
            };

        public IEnumerable<string> ExpectedDiscardedDir1 { get; }
            = new[]
            {
                @"TestDir1\berlin - Copy.mp4",
                @"TestDir1\SubDir1\lake.jpg",
                @"TestDir1\SubDir1\plaque.jpg",
                @"TestDir1\SubDir1\SubDir2\mountain-differentName.jpg"
            };

        public IEnumerable<string> ExpectedDuplicatesDir2 { get; }
            = new[]
            {
                @"TestDir2\SubDir1\mountain.jpg",
                @"TestDir2\SubDir1\park.jpg",
                @"TestDir2\SubDir1\SubDir2\berlin.mp4",
                @"TestDir2\SubDir1\SubDir2\lake.jpg"
            };

        public IEnumerable<string> UniqueFilesDir2 { get; }
            = new[]
            {
                @"TestDir2\protest.jpg"
            };


        [SetUp]
        public void Setup()
        {
            var fileSystem = new FileSystem();
            fileSystem.File.Delete(TestDbPath);
        }

        [Test]
        public void TestAddToDb()
        {
            var fileComparer = new FileComparer(TestDbPath);
            var filesAdded = fileComparer.AddToDb(TestDir1);

            var result = filesAdded.Select(path => Path.GetRelativePath(TestRootDirectory, path));

            CollectionAssert.AreEquivalent(ExpectedAddedDir1, result);
            CollectionAssert.IsNotSubsetOf(ExpectedDiscardedDir1, result);
        }

        // Wanted to try on actual files directly

        //[Test]
        //public void Test1()
        //{
        //    var fileSystemMock = new MockFileSystem();

        //    var fileComparer = new FileComparer(fileSystemMock, "");

        //    Assert.AreEqual(true, true, "The files are not equal.");
        //}
    }
}
