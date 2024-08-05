using MoviesMenuSql.Models;
using System.Data.SqlClient;

namespace MoviesMenuSql.Services;

internal class MovieService
{
    private DbService dbService = new();
    private List<Movie> movies = [];

    public MovieService() => this.updateInitialMoviesList();

    public void updateInitialMoviesList()
    {
        List<Movie> initialMovies = dbService.GetInitialMovies();
        movies = initialMovies;
    }

    public List<Movie> ListAllMovies() => movies;
    

    //public string AddMovie(Movie movie)
    //{
    //    movie.Id = movies.Count + 1;
    //    movies.Add(movie);
    //    return "Movie added successfully.";
    //}

    public string AddMovie(Movie movie)
    {
        using SqlConnection connection = dbService.GetConnection();
        try
        {
            connection.Open();

            string insertQuery = @"
                INSERT INTO Movies (Title, Director, ReleaseYear, Genre, Price)
                VALUES (@Title, @Director, @ReleaseYear, @Genre, @Price)";

            SqlCommand insertCommand = new(insertQuery, connection);
            insertCommand.Parameters.AddWithValue("@Title", movie.Title);
            insertCommand.Parameters.AddWithValue("@Director", movie.Director);
            insertCommand.Parameters.AddWithValue("@ReleaseYear", movie.ReleaseYear);
            insertCommand.Parameters.AddWithValue("@Genre", movie.Genre);
            insertCommand.Parameters.AddWithValue("@Price", movie.Price);

            int result = insertCommand.ExecuteNonQuery();

            updateInitialMoviesList();

            string successMsg = $"{movie.Title} added to the database successfully!";
            return successMsg + $" {result} rows inserted.";
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
        finally
        {
            connection.Close();
        }
    }

    public string ModifyMovie(Movie updatedMovie)
    {
        var movie = movies.FirstOrDefault(m => m.Id == updatedMovie.Id);
        if (movie == null)
            return "Movie not found.";


        movie.Title = updatedMovie.Title;
        movie.Director = updatedMovie.Director;
        movie.Genre = updatedMovie.Genre;
        movie.ReleaseYear = updatedMovie.ReleaseYear;
        movie.Price = updatedMovie.Price;
        return "Movie updated successfully.";
    }

    public string RemoveMovie(int? id)
    {
        var movie = movies.FirstOrDefault(m => m.Id == id);
        if (movie == null)
            return "Movie not found.";

        movies.Remove(movie);
        return $"Movie: {movie.Title} removed from the list";
    }

    public bool CheckMovieExists(int? id)
    {
        return movies.Any(m => m.Id == id);
    }

}
