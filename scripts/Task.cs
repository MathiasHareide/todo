public class Task
{
    public Task(string title, string description)
    {
        Name = title;
        Description = description;
    }

    public readonly string Name = "";
    public readonly string Description = "";
    public bool Completed {get; private set;} = false;

    public void ToggleCompletion()
    {
        Completed = !Completed;
    }
}