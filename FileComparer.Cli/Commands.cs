using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCompare.Cli
{
    public interface ICommand
    {
        void Execute();
    }


    [Verb("add", HelpText = "Add files from a directory (recursively) into the db.")]
    public class AddToDbCommand : ICommand
    {
        [Option('f', "folder", Required = true, HelpText = "Specify the target directory")]
        public string Directory { get; set; }

        [Option('d', "dbPath", Required = true, HelpText = "Specify the full path to the db (will be created if does not exist)")]
        public string DbPath { get; set; }

        public void Execute()
        {
            Console.WriteLine("Scanning directory...");

            var comparer = new FileComparer(DbPath);

            comparer.AddToDb(Directory);

            Console.WriteLine("Files successfully added to the db.");
        }
    }


    [Verb("search", HelpText = "Search a directory (recursively) for duplicates of known files from the db.")]
    public class SearchForDuplicatesCommand : ICommand
    {
        [Option('f', "folder", Required = true, HelpText = "Specify the directory to search")]
        public string Directory { get; set; }

        [Option('d', "dbPath", Required = true, HelpText = "Specify the full path to the db (db should be populated)")]
        public string DbPath { get; set; }

        public void Execute()
        {
            Console.WriteLine("Searching directory for known files...");

            var comparer = new FileComparer(DbPath);

            var duplicatesReport = comparer.SearchForDuplicates(Directory);

            //Print files with duplicates

            if (!duplicatesReport.Any())
            {
                Console.WriteLine("Found no duplicate of any known file previously added to the db.");
            }

            foreach (var report in duplicatesReport)
            {
                Console.WriteLine();
                Console.WriteLine($"Found duplicate(s) of known file {report.Key} at:");

                foreach (var duplicate in report.Value)
                {
                    Console.WriteLine($"\t{duplicate}");
                }
            }
        }
    }
}
