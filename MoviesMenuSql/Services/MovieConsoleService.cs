using System.Data.SqlClient;
using MoviesMenuSql.Models;
using MoviesMenuSql.Services;

namespace MoviesMenuSql.Services;

internal class MovieConsoleService
{
    private readonly MovieService _movieService;
    private DbService dbService = new();

    public MovieConsoleService(MovieService movieService) => _movieService = movieService;

    public void DisplayMenu(List<Option> menuOptions, int selectedIndex)
    {
        Menu.WriteMenu(menuOptions, menuOptions[selectedIndex]);
    }

    public void HandleMenuSelection(List<Option> menuOptions)
    {
        int index = 0;
        ConsoleKeyInfo keyInfo;
        do
        {
            keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.DownArrow)
            {
                if (index + 1 < menuOptions.Count)
                {
                    index++;
                    DisplayMenu(menuOptions, index);
                }
            }
            if (keyInfo.Key == ConsoleKey.UpArrow)
            {
                if (index - 1 >= 0)
                {
                    index--;
                    DisplayMenu(menuOptions, index);
                }
            }
            if (keyInfo.Key == ConsoleKey.Enter)
            {
                menuOptions[index].Selected.Invoke();
                index = 0;
                DisplayMenu(menuOptions, index); // Redisplay menu after an option is executed
            }
        }
        while (keyInfo.Key != ConsoleKey.X);
    }

    public void ListAllMovies()
    {
        Console.Clear();

        var moviesToPrint = new List<Movie>();
        foreach (Movie movie in _movieService.ListAllMovies())
            moviesToPrint.Add(movie);

        IEnumerable<string> listMovieQuery =
            from movie in moviesToPrint
            select $"Id: {movie.Id}, Title: {movie.Title}, Director: {movie.Director}, Genre: {movie.Genre}, Release Year: {movie.ReleaseYear}, Price: {movie.Price}";

        foreach (string movie in listMovieQuery)
            Console.WriteLine(movie);

        Console.WriteLine("\n\nPress 'b' to go back to the main menu.");

        while (true)
        {
            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.B)
                break;
        }
    }

    public void AddMovie()
    {
        Console.Clear();

        Console.WriteLine("Enter movie title:");
        string? title = Console.ReadLine();

        Console.WriteLine("Enter director name:");
        string? director = Console.ReadLine();

        Console.WriteLine("Enter genre:");
        string? genre = Console.ReadLine();

        int releaseYear;
        while (true)
        {
            Console.WriteLine("Enter release year:");
            if (int.TryParse(Console.ReadLine(), out releaseYear))
                break;
            
            Console.WriteLine("Invalid input. Please enter a valid release year.");
        }

        decimal price;
        while (true)
        {
            Console.WriteLine("Enter price:");
            if (decimal.TryParse(Console.ReadLine(), out price))
                break;

            Console.WriteLine("Invalid input. Please enter a valid price.");
        }

        string? result = _movieService.AddMovie(new Movie(0, title, director, genre, releaseYear, price));
        Console.WriteLine("\n" + result);

        Console.WriteLine("\n\nPress 'b' to go back to the main menu.");

        while (true)
        {
            if (Console.ReadKey(true).Key == ConsoleKey.B)
                break;
        }
    } // end of AddMovie()


    public void ModifyMovie()
    {
        Console.Clear();

        var moviesToPrint = new List<Movie>();
        foreach (Movie movie in _movieService.ListAllMovies())
            moviesToPrint.Add(movie);

        IEnumerable<string> listMovieQuery =
            from movie in moviesToPrint
            select $"Id: {movie.Id}, Title: {movie.Title}, Director: {movie.Director}, Genre: {movie.Genre}, Release Year: {movie.ReleaseYear}, Price: {movie.Price}";

        foreach (string movie in listMovieQuery)
            Console.WriteLine(movie);

        int id;
        while (true)
        {
            Console.WriteLine("\n\nEnter the ID of the movie to modify:");
            if (int.TryParse(Console.ReadLine(), out id) && _movieService.CheckMovieExists(id))
                break;

            Console.WriteLine("Invalid input or movie doesn't exist. Please enter a valid movie ID.");
        }

        Console.WriteLine("Enter movie title:");
        string? title = Console.ReadLine();

        Console.WriteLine("Enter director name:");
        string? director = Console.ReadLine();

        Console.WriteLine("Enter genre:");
        string? genre = Console.ReadLine();

        int releaseYear;
        while (true)
        {
            Console.WriteLine("Enter release year:");
            if (int.TryParse(Console.ReadLine(), out releaseYear))
                break;

            Console.WriteLine("Invalid input. Please enter a valid release year.");
        }

        decimal price;
        while (true)
        {
            Console.WriteLine("Enter price:");
            if (decimal.TryParse(Console.ReadLine(), out price))
                break;

            Console.WriteLine("Invalid input. Please enter a valid price.");
        }

        string results = _movieService.ModifyMovie(new Movie(id, title, director, genre, releaseYear, price));
        Console.WriteLine("\n" + results);

        Console.WriteLine("\n\nPress 'b' to go back to the main menu.");

        while (true)
        {
            if (Console.ReadKey(true).Key == ConsoleKey.B)            
                break;
        }
    } // end of ModifyMovie()



    public void RemoveMovie()
    {
        Console.Clear();

        var moviesToPrint = new List<Movie>();
        foreach (Movie movie in _movieService.ListAllMovies())
            moviesToPrint.Add(movie);

        IEnumerable<string> listMovieQuery =
            from movie in moviesToPrint
            select $"Id: {movie.Id}, Title: {movie.Title}, Director: {movie.Director}, Genre: {movie.Genre}, Release Year: {movie.ReleaseYear}, Price: {movie.Price}";

        foreach (string movie in listMovieQuery)
            Console.WriteLine(movie);

        Console.WriteLine("\n\nEnter the ID of the movie to delete: ");

        int id;
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out id) && _movieService.CheckMovieExists(id))
                break;
            
            Console.WriteLine("Invalid input or movie doesn't exist. Please enter a valid movie ID.");
        }

        string result = _movieService.RemoveMovie(id);
        Console.WriteLine(result);

        Console.WriteLine("\n\nPress 'b' to go back to the main menu.");

        // press b to go back to main menu
        while (true)
        {
            if (Console.ReadKey(true).Key == ConsoleKey.B)
                break;
        }
    } // end of RemoveMovie()

    public void PrintPersons()
    {
        Console.Clear();

        using (SqlConnection connection = dbService.GetConnection())
        {
            try
            {
                connection.Open();
                Console.WriteLine("Connection opened successfully.");

                string selectQuery = "SELECT * FROM Person";
                SqlCommand command = new SqlCommand(selectQuery, connection);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                    Console.WriteLine($"Id: {reader["id"]}, Name: {reader["name"]}, Created: {reader["CreateDateTime"]}");
                
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
                Console.WriteLine("Connection closed.");
            }
        }

        Console.WriteLine("\n\nPress 'b' to go back to the main menu.");

        // press b to go back to main menu
        while (true)
        {
            if (Console.ReadKey(true).Key == ConsoleKey.B)
                break;
        }
    } // end of PrintNames()

}
