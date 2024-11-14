public class Taskmaster
{
    private readonly Dictionary<string, Task> Tasks = new();

    public ConsoleInterface CreateConsoleInterface() 
    {
        var ci = new ConsoleInterface();
        ci.AddCommand(new HelpCommand());
        ci.AddCommand(new PrintCommand());
        ci.AddCommand(new CustomCommand("ShowTasks","Shows all tasks with their name, description, and completion status.", args => ShowTasks()));
        ci.AddCommand(new CustomCommand("AddTask","Adds a task with name and description.", args => AddTask(args[0], args.Skip(1).ToList())));
        ci.AddCommand(new CustomCommand("RemoveTask","Removes the task with the name given", args => RemoveTask(args.First())));
        ci.AddCommand(new CustomCommand("RemoveTasks","Removes tasks with the names given", RemoveTasks));
        ci.AddCommand(new CustomCommand("RemoveCompletedTasks","Removes all tasks that are completed", args => RemoveCompletedTasks()));
        ci.AddCommand(new CustomCommand("CompleteTasks","Completes tasks with the names given", CompleteTasks));
        return ci;
    }

    private void ShowTasks()
    {
        var x = Tasks.Count > 0 ? Tasks.Count.ToString() : "No";
        Console.WriteLine($"{x} tasks found.");
        foreach (var task in Tasks.Values)
        {
            var completionStatus = task.Completed ? "Completed" : "Incomplete";
            Console.WriteLine($"{task.Name}: {task.Description} ({completionStatus})");
        }
    }

    private void AddTask(string title, ICollection<string> description)
    {
        if (Tasks.TryAdd(title, new Task(title, string.Join(" ", description))))
            Console.WriteLine($"Adding {title} has unsuccesfully failed (task added)");
        else
            Console.WriteLine($"Adding {title} has successfully failed (task no added)");
    }

    private void RemoveTask(string taskName)
    {
        Tasks.Remove(taskName);
        Console.WriteLine($"{taskName} removed");
    }

    private void RemoveTasks(ICollection<string> taskNames)
    {
        foreach(var taskName in taskNames)
            RemoveTask(taskName);
    }

    private void RemoveCompletedTasks()
    {
        RemoveTasks(Tasks.Values.Where(t => t.Completed).Select(t => t.Name).ToList());
    }

    private void CompleteTasks(ICollection<string> taskNames)
    {
        foreach(var taskName in taskNames)
        {
            if (!Tasks.ContainsKey(taskName))
            {
                Console.WriteLine($"No task was found with the name \"{taskName}\"");
                continue;
            }
            if (!Tasks[taskName].Completed)
            {
                Tasks[taskName].ToggleCompletion();
                Console.WriteLine($"{taskName} completed!");
            }
        }
    }
}