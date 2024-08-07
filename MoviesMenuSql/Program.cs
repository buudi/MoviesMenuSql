using MoviesMenuSql.Models;
using MoviesMenuSql.Services;

namespace MoviesMenuSql;

class Program
{
    public static List<Option> MenuOptions = [];
    public static MovieService movieService = new();
    public static MovieConsoleService movieConsoleService = new(movieService);

    static void Main(string[] args)
    {
        // Movie Menu Options
        MenuOptions =
        [
            new Option("List all available movies.", movieConsoleService.ListAllMovies),
            new Option("Add a new movie to the list.", movieConsoleService.AddMovie),
            new Option("Modify an existing movie.", movieConsoleService.ModifyMovie),
            new Option("Remove a movie from the list", movieConsoleService.RemoveMovie),
            new Option("Print Names", movieConsoleService.PrintPersons),
            new Option("Exit the program.", () => Environment.Exit(0))
        ];

        // Set the default index of the selected item to be the first
        int index = 0;

        // Write the menu out
        movieConsoleService.DisplayMenu(MenuOptions, index);
        movieConsoleService.HandleMenuSelection(MenuOptions);

        Console.ReadKey();
    }
}
