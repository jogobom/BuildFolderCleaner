if (args.Length == 0)
{
    Console.WriteLine($"No argument supplied, I refuse to run so I don't accidentally delete a load of things you didn't intend.");
    return;
}

var dirToSearch = args[0];

var foldersToDelete =
    Directory.EnumerateDirectories(dirToSearch, "bin", SearchOption.AllDirectories).Concat(
        Directory.EnumerateDirectories(dirToSearch, "obj", SearchOption.AllDirectories))
        .Where(f => !f.Contains("node_modules"))
        .ToList();

Console.WriteLine("I'm going to delete the following folders:");

foreach (var folder in foldersToDelete)
{
    Console.WriteLine(folder);
}

const string confirmCode = "order 66";
Console.WriteLine($"Say '{confirmCode}' to confirm:");

if (Console.ReadLine() != confirmCode)
{
    Console.WriteLine("Those folders will not die this day.");
    return;
}
    
foreach (var folder in foldersToDelete)
{
    try
    {
        Directory.Delete(folder, true);
        Console.WriteLine($"Deleted {folder}");
    }
    catch(Exception e)
    {
        Console.WriteLine($"Unable to delete {folder} because {e.Message}");
    }
}
