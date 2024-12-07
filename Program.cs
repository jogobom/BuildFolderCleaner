using Spectre.Console;

if (args.Length == 0)
{
    AnsiConsole.MarkupLine($"[yellow]No argument supplied, I refuse to run so I don't accidentally delete a load of things you didn't intend.[/]");
    return;
}

var dirToSearch = args[0];

var foldersToDelete =
    Directory.EnumerateDirectories(dirToSearch, "bin", SearchOption.AllDirectories).Concat(
        Directory.EnumerateDirectories(dirToSearch, "obj", SearchOption.AllDirectories))
        .Where(f => !f.Contains("node_modules"))
        .ToList();

AnsiConsole.MarkupLine("I'm going to [bold]delete[/] the following folders:");

foreach (var folder in foldersToDelete)
{
    Console.WriteLine(folder);
}

const string confirmCode = "order 66";
AnsiConsole.MarkupLine($"[green]Say [bold]'{confirmCode}'[/] to confirm:[/]");

if (Console.ReadLine() != confirmCode)
{
    AnsiConsole.MarkupLine("[green]Those folders will not die this day.[/]");
    return;
}
    
foreach (var folder in foldersToDelete)
{
    try
    {
        Directory.Delete(folder, true);
        AnsiConsole.MarkupLine($"Deleted {folder}");
    }
    catch(Exception e)
    {
        AnsiConsole.MarkupLine($"[red]Unable to delete {folder} because [bold]{e.Message}[/][/]");
    }
}
