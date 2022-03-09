# Tackling the task

My process:
- Do some research, get inspired.
- List ideas and questions.
- Set the scope, priorities.
- Go for it.
- Document limitations and potential improvements.

Priorities during development:
1. fulfill the basic requirements of the features,
2. refactor for maintainability,
3. optimize performance.


# Main goal

A tool capable of identifying duplicate binary files in a directory.


# Features (v0)

## Scan a directory (AddToDB)

Reads all binary files in a directory (recursively).
Produces a hash of each file's contents (without system metadata).
Stores this hash together with the file name (full path).
Storage is done in a simple text file (FileCompare.csv), in the directory.

## Search for duplicates (SearchForDuplicates)

Looks in the db for duplicate values of each hash.
Outputs the names of the files that are duplicates of each other (shown as a group).

A 'duplicate' here is a file whose contents (without system metadata) are exactly the same as another file.
Comparing hashes of file contents is considered safe and accurate enough to find duplicates.
Some binary files have embedded application metadata, and may be counted as distinct even though the main contents are equal.

## Overall user experience

- When searching for duplicates, if the scan wasn't performed yet, ask to perform it,
- Responsiveness in the CLI indicating what it's doing and giving signs of life,
- Basic error handling.


# Potential improvements

## Overall user experience

- Options such as --verbose,
- Save outputs to a file.

## Scan a directory (AddToDB)

- Support cross-directory search. E.g. scan and search dir1, then scan dir 2 and search across dir1 and dir2.
- Support/prevent/warn of changes haveing been made to the directory between the scan and the search.

## Search for duplicates (SearchForDuplicates)

- Support a spectrum of file similarity, e.g. hard duplicates vs soft duplicates ('similar' files), by analyzing metadata and actual file contents.
