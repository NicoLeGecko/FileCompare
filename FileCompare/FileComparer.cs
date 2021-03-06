using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

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
            _context.Database.EnsureCreated();
        }

        /// <summary>
        /// Finds files in the directory and add to the db so that they are later returned by a search.
        /// </summary>
        /// <param name="directory"></param>
        public IEnumerable<string> AddToDb(string directory)
        {
            var fileEntries = ScanDirectory(directory);

            var distinctEntries = fileEntries
                .OrderBy(i => i.FullPath.Length)
                .DistinctBy(e => e.HashAsText);

            var existingHashes = _context.FileEntries
                .Select(e => e.HashAsText)
                .ToList();

            var entriesToAdd = distinctEntries.ExceptBy(existingHashes, e => e.HashAsText);

            Console.WriteLine();
            if (entriesToAdd.Any())
            {
                _context.FileEntries.AddRange(entriesToAdd);
                _context.SaveChanges();

                Console.WriteLine($"Adding {entriesToAdd.Count()} new files to the db:");
                foreach (var entry in entriesToAdd)
                {
                    Console.WriteLine($"\t{entry.FullPath}");
                } 
            }
            else
            {
                Console.WriteLine("No new files to add to the db.");
            }

            return distinctEntries.Select(e => e.FullPath);
        }

        /// <summary>
        /// Searches a directory for files that have a binary duplicate in the db.
        /// </summary>
        /// <param name="directory"></param>
        /// <returns>A dictionary with the files in the scanned directory togethee with the found duplicates in the db</returns>
        public Dictionary<string, IEnumerable<string>> SearchForDuplicates(string directory)
        {
            var dict = new Dictionary<string, IEnumerable<string>>();
            
            var newEntries = ScanDirectory(directory);

            var knownEntries = _context.FileEntries
                .Where(e => newEntries.Select(n => n.HashAsBytes).Contains(e.HashAsBytes));

            foreach (var knownEntry in knownEntries)
            {
                IEnumerable<string> added = newEntries.
                        Where(e => e.HashAsBytes.SequenceEqual(knownEntry.HashAsBytes))
                        .Select(e => e.FullPath);

                dict.Add(knownEntry.FullPath, added);
            }

            return dict;
        }


        private IEnumerable<FileEntry> ScanDirectory(string directory)
        {
            var directoryInfo = _fileSystem.DirectoryInfo.FromDirectoryName(directory);
            var fileInfos = directoryInfo.EnumerateFiles("*", System.IO.SearchOption.AllDirectories);

            var fileEntries = fileInfos
                .AsParallel()
                .Select(i =>
                {
                    using (SHA1 sha = SHA1.Create())
                    using (FileStream fs = new(i.FullName, FileMode.Open, FileAccess.Read))
                    {
                        var hash = sha.ComputeHash(fs);

                        return new FileEntry()
                        {
                            HashAsBytes = hash,
                            FullPath = i.FullName
                        };
                    };
                });

            return fileEntries.AsEnumerable();
        }

    }
}
