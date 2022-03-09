using System;
using System.Collections.Generic;
using System.IO.Abstractions;

namespace FileCompare
{
    public class FileComparer
    {
        private readonly IFileSystem _fileSystem;
        private readonly FileCompareContext _context;

        /// <summary>
        /// Number of files in db
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Creates a FileComparer and loads the db from the supplied path
        /// </summary>
        /// <param name="dbPath">Path to were the data is stored</param>
        public FileComparer(string dbPath) : this(new FileSystem(), dbPath)
        {

        }

        /// <summary>
        /// Constructor that takes a mockable IFileSystem for use in tests
        /// </summary>
        /// <param name="fileSystem"></param>
        /// <param name="dbPath"></param>
        internal FileComparer(IFileSystem fileSystem, string dbPath)
        {
            _fileSystem = fileSystem;

            _context = new FileCompareContext(dbPath);
        }

        /// <summary>
        /// Finds files in the directory and add to the db so that they are later returned by a search.
        /// </summary>
        /// <param name="directory"></param>
        public void AddToDb(string directory)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Searches a directory for files that have a binary duplicate in the db.
        /// </summary>
        /// <param name="directory"></param>
        /// <returns>A dictionary with the files in the scanned directory togethee with the found duplicates in the db</returns>
        public Dictionary<string, IEnumerable<string>> SearchForDuplicates(string directory)
        {
            throw new NotImplementedException();
        }
    }
}
