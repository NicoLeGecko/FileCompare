// See https://aka.ms/new-console-template for more information

using CommandLine;
using FileCompare.Cli;

//Add validation that the entered arguments are valid

Parser.Default.ParseArguments<AddToDbCommand, SearchForDuplicatesCommand>(args)
        .WithParsed<ICommand>(t => t.Execute());