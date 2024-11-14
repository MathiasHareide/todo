public class PrintCommand : ICommand
{
    public string Name => "print";
    public string Description => "Prints all arguments with a space inbetween";

    public void Execute(List<string> args)
    {
        Console.WriteLine(string.Join(" ", args));
    }
}

public class HelpCommand : ICommand
{
    public string Name => "help";
    public string Description => "Shows all commands";

    public void Execute(List<string>? args)
    {
        ConsoleInterface.Instance?.DisplayAllCommands();
    }
}

public class CustomCommand : ICommand
{
    public string Name { get; private set; }
    public string Description { get; private set;}

    private Action<List<string>> _execute;
    
    public CustomCommand(string name, string description, Action<List<string>> execute)
    {
        Name = name;
        Description = description;
        _execute = execute;
    }

    public void Execute(List<string> args) => _execute.Invoke(args);
}
