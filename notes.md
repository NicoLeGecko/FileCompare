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

A tool capable of identifying binary files in a directory that are duplicates of files previously known.
E.g. scan dir1, add files to the db. Then scan dir 2 and search look for duplicates of files from dir1.


# Features (v0)

## Initialization

Loads or creates a db (sqlite) at a given path.

## Scan a directory (AddToDB)

Reads all binary files in a directory (recursively).
Produces a hash of each file's contents (without system metadata).
Stores this hash together with the file name (full path) in the db.
Avoids storing duplicates (chooses the file with shortest full path).

The hashing algorithm is SHA1 as I've understood it's the fastest. Neglecting the risk of hash collisions.

## Search for duplicates (SearchForDuplicates)

Reads from a new directory, and looks in the db for duplicate values of each hash.
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

- Search hidden files
- Ignore text files

## Search for duplicates (SearchForDuplicates)

- After search is done, suggest to add the non-duplicate files to the db,
- Support a spectrum of file similarity, e.g. hard duplicates vs soft duplicates ('similar' files), by analyzing metadata and actual file contents.

## Code

- Decouple using interfaces and dependency injection, for maintainability and unit-testability
- Write overrides for equality comparers of FileEntry
- Use the hash as primary key directly
- Optimize the comparison of hashes and collections