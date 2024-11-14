using System.Diagnostics;

public class ConsoleInterface
{
    public static ConsoleInterface ?Instance {get; private set;}

    private readonly Dictionary<string, ICommand> _commands = new();

    public void Start()
    {
        Instance = this;
        Console.WriteLine("Hello! Welcome. To perform a command write the name of the command followed by arguments if needed.\nHere are all available commands:");
        DisplayAllCommands();
        do
        {
            var input = Console.ReadLine()?.Split(' ');
            if (input == null) continue;
            var cmdName = input.First();
            var args = input.Skip(1).ToList();
            Debug.Assert(input.Length <= 1 || args.Count == input.Length - 1);
            if (!_commands.TryGetValue(cmdName.ToLower(), out var cmd))
                Console.WriteLine("Invalid input!");
            else
                cmd?.Execute(args);
        }
        while (Instance == this);
    }

    public void AddCommand(ICommand cmd)
    {
        _commands.Add(cmd.Name.ToLower(), cmd);
    }

    public void RemoveCommand(ICommand cmd)
    {
        _commands.Remove(cmd.Name);
    }
    public void RemoveCommand(string cmdName)
    {
        _commands.Remove(cmdName.ToLower());
    }

    public void DisplayAllCommands()
    {
        foreach (var cmd in _commands)
            Console.WriteLine($"{cmd.Key}: {cmd.Value.Description}");
    }
}
