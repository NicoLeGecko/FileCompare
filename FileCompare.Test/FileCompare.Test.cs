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
        public string TestDirectory => "TestDirectory";

        public IEnumerable<string> RelativePathsToDuplicatedTestFiles { get; }
            = new List<string>
            {
                @"berlin.mp4",
                @"berlin - Copy.mp4",
                @"SubDir1\berlin.mp4",
                @"SubDir1\SubDir2\berlin_copycopy.mp4",
                //
                @"lake.jpg",
                @"lake - Copy.jpg",
                @"SubDir1\lake.jpg",
                @"SubDir1\lake - Copy.jpg",
                //
                @"mountain.jpg",
                @"mountain - Copy.jpg",
                @"SubDir1\mountain.jpg",
                @"SubDir1\mountain - Copy.jpg",
                @"SubDir1\SubDir2\mountain_copycopy.jpg",
                //
                @"park.jpg",
                @"park - Copy.jpg",
                @"SubDir1\park.jpg",
                @"SubDir1\park - Copy.jpg",
                @"SubDir1\SubDir2\park.jpg"
            };

        public IEnumerable<string> RelativePathsToUniqueTestFiles { get; }
           = new List<string>
           { 
               @"protest.jpg",
               @"SubDir1\plaque.jpg",
               @"SubDir1\SubDir2\moon.jpg"
           };

        [Test]
        /// <summary>
        /// Sanity test
        /// </summary>
        public void ListFilesInDirectory()
        {
            var fileSystem = new FileSystem();

            var testDirectoryInfo = fileSystem.DirectoryInfo.FromDirectoryName(TestDirectory);

            var fileInfos = testDirectoryInfo.EnumerateFiles("*", System.IO.SearchOption.AllDirectories);

            Console.WriteLine($"Found {fileInfos.Count()} files in " + testDirectoryInfo.Name);

            foreach (var fileInfo in fileInfos)
            {
                Console.WriteLine(fileInfo.FullName);
            }

            int expected = RelativePathsToUniqueTestFiles.Count() + RelativePathsToDuplicatedTestFiles.Count();

            Assert.AreEqual(expected, fileInfos.Count());
        }



        // Want to try on actual files directly

        //[Test]
        //public void Test1()
        //{
        //    var fileSystemMock = new MockFileSystem();

        //    var fileComparer = new FileComparer(fileSystemMock, "");

        //    Assert.AreEqual(true, true, "The files are not equal.");
        //}
    }
}
