using MoviesMenuSql.Models;
using System.Data.SqlClient;
using System.Net.NetworkInformation;

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
        using SqlConnection connection = dbService.GetConnection();
        try
        {
            connection.Open();

            string updateQuery = "UPDATE Movies SET Title=@Title, Director=@Director, Genre=@Genre, ReleaseYear=@ReleaseYear, Price=@Price WHERE Id=@Id";
            SqlCommand updateCommand = new(updateQuery, connection);
            updateCommand.Parameters.AddWithValue("@Title", updatedMovie.Title);
            updateCommand.Parameters.AddWithValue("@Director", updatedMovie.Director);
            updateCommand.Parameters.AddWithValue("@Genre", updatedMovie.Genre);
            updateCommand.Parameters.AddWithValue("@ReleaseYear", updatedMovie.ReleaseYear);
            updateCommand.Parameters.AddWithValue("@Price", updatedMovie.Price);
            updateCommand.Parameters.AddWithValue("@Id", updatedMovie.Id);


            int result = updateCommand.ExecuteNonQuery();
            updateInitialMoviesList();

            string successMsg = $"{updatedMovie.Title} modified in the database successfully!";
            return successMsg + $" {result} rows updated.";
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

    public string RemoveMovie(int? id)
    {
        using SqlConnection connection = dbService.GetConnection();
        try
        {
            connection.Open();
            string deleteQuery = "DELETE FROM Movies WHERE Id = @Id";
            SqlCommand deleteCommand = new(deleteQuery, connection);

            deleteCommand.Parameters.AddWithValue("@Id", id);
            int result = deleteCommand.ExecuteNonQuery();
            updateInitialMoviesList();

            string successMsg = $" Movie with Id: {id} deleted from the database successfully!";
            return successMsg + $" {result} rows deleted.";


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

    public bool CheckMovieExists(int? id)
    {
        return movies.Any(m => m.Id == id);
    }

}
