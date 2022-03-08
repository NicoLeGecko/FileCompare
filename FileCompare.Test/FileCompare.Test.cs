using NUnit.Framework;
using FileCompare;
using System.IO.Abstractions.TestingHelpers;

namespace FileCompare.Test
{
    [TestFixture]
    public class FileCompare_Test
    {
        [Test]
        public void Test1()
        {
            var fileSystemMock = new MockFileSystem();
            
            var fileComparer = new FileComparer(fileSystemMock, );

            Assert.AreEqual(true, true, "The files are not equal.");
        }
    }
}
