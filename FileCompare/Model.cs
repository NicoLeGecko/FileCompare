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

        public FileCompareContext(string dbPath = "fileCompare.db")
        {
            DbPath = dbPath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }

    public class FileEntry
    {
        public int FileEntryId { get; set; }
        public string FullPath { get; set; }
        public byte[] HashAsBytes { get; set; }
        
        public string HashAsText => Encoding.Default.GetString(HashAsBytes);

        public override string ToString()
        {
            return $"Hash:{HashAsText} - Path: {FullPath}";
        }
    }
}
