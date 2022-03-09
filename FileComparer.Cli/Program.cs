// See https://aka.ms/new-console-template for more information

using FileCompare;

//Add validation that the entered arguments are valid



var command = args[0];
var directory = args[1];
var dbPath = args[2];

var comparer = new FileComparer(dbPath);

switch (command)
{
    case "add":
        Console.WriteLine("Adding to db");
        AddToDb(directory);
        break;
    case "search":
        Console.WriteLine("Searching");
        Search(directory);
        break;
    default:
        Console.WriteLine("Unknown command. Please try 'add' or 'search' ");
        break;
        // Inform user of unknown command
}

void AddToDb(string directory) => comparer.AddToDb(directory);

void Search(string directory)
{
    var duplicatesReport = comparer.SearchForDuplicates(directory);

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
