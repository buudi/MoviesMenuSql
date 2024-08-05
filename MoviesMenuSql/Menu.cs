namespace MoviesMenuSql;

public static class Menu
{
    public static void WriteMenu(List<Option> options, Option selectedOption)
    {
        Console.Clear();

        foreach (Option option in options)
        {
            if (option == selectedOption)
                Console.Write("> ");

            else
                Console.Write(" ");

            Console.WriteLine(option.Name);
        }
    }
}

