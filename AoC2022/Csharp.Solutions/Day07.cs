namespace Csharp.Solutions;

public static class Day07
{
    public static int FirstPuzzle(IEnumerable<ICommandLineEntry> instructions)
    {
        var root = GetFileTree(instructions.Skip(1));
        GetDirectorySizes(root);
        return Sizes.Where(s => s <= 100000).Sum();
    }
    
    public static int SecondPuzzle(IEnumerable<ICommandLineEntry> instructions)
    {
        var root = GetFileTree(instructions.Skip(1));
        GetDirectorySizes(root);
        var freeSpace = 70000000 - GetDirectorySize(root);
        var neededSpace = 30000000 - freeSpace;
        return Sizes.Where(s => s >= neededSpace).Min();
    }

    private static int GetDirectorySize(Directory dir) =>
        dir.Directories.Sum(GetDirectorySize) + dir.Files.Sum(f => f.Size);

    private static Directory GetFileTree(IEnumerable<ICommandLineEntry> instructions)
    {
        var rootDirectory = new Directory { Name = "/", ParentDirectory = null };
        var currentDirectory = rootDirectory;
        foreach (var instruction in instructions)
        {
            switch (instruction)
            {
                case SwitchDirectoryUpCommand:
                    currentDirectory = currentDirectory.ParentDirectory;
                    break;
                case SwitchDirectoryCommand switchDirectoryCommand:
                    currentDirectory =
                        currentDirectory.Directories.First(d => d.Name == switchDirectoryCommand.NameOfDirectory);
                    break;
                case ListContentsCommand:
                    break;
                case FileListing fileListing:
                    currentDirectory.Files.Add(new File{Name = fileListing.Name, Size = fileListing.Size});
                    break;
                case DirectoryListing directoryListing:
                    currentDirectory.Directories.Add(new Directory{Name = directoryListing.Name, ParentDirectory = currentDirectory});
                    break;
            }
        }

        return rootDirectory;
    }

    private static List<int> Sizes { get; } = new();

    private static void GetDirectorySizes(Directory directory)
    {
        Sizes.Add(GetDirectorySize(directory));
        foreach (var subDirectory in directory.Directories)
        {
            GetDirectorySizes(subDirectory);
        }
    }
}

public struct File
{
    public string Name { get; init; }
    public int Size { get; init; }
}

public class Directory
{
    public string Name { get; init; }
    public List<File> Files { get; } = new();
    public List<Directory> Directories { get; } = new();
    public Directory? ParentDirectory { get; init; }
}

public interface ICommandLineEntry{}

public class SwitchDirectoryUpCommand : ICommandLineEntry
{
}

public class SwitchDirectoryCommand : ICommandLineEntry
{
    public string NameOfDirectory { get; init; }
}

public class ListContentsCommand : ICommandLineEntry
{
}

public class FileListing : ICommandLineEntry
{
    public string Name { get; init; }
    public int Size { get; init; }
}

public class DirectoryListing : ICommandLineEntry
{
    public string Name { get; init; }
}