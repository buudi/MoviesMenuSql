namespace MoviesMenuSql;

public class Option
{
    public string Name { get; }

    // we use Action delegate since Selected has no parameters and doesn't return a value
    public Action Selected;

    public Option(string name, Action selected)
    {
        Name = name;
        Selected = selected;
    }
}
