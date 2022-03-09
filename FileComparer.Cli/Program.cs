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
        AddToDb(directory);
        break;
    case "search":
        Search(directory);
        break;
    default:
        break;
        // Inform user of unknown command
}

void AddToDb(string directory) => comparer.AddToDb(directory);

void Search(string directory)
{
    var duplicates = comparer.SearchForDuplicates(directory);
    //Print files with duplicates
}
