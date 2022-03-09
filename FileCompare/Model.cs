using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCompare
{
    public class FileCompareContext : DbContext
    {
        public DbSet<FileEntry> FileEntries { get; set; }

        public string DbPath { get; }

        public FileCompareContext(string dbPath)
        {
            DbPath = dbPath;
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }

    public class FileEntry
    {
        public int FileEntryId { get; set; }
        public string ContentHash { get; set; }
        public string FullPath { get; set; }

        public override string ToString()
        {
            return $"Id:{FileEntryId} - Hash:{ContentHash} - Path: {FullPath}";
        }
    }
}
