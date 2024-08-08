using System.Data.SqlClient;
using MoviesMenuSql.Models;

namespace MoviesMenuSql.Services;

public class DbService
{
    private readonly string _connectionString;
    public DbService() => _connectionString = "Server=localhost\\SQLEXPRESS;Database=MyBootcamp;Trusted_Connection=True;";
    public SqlConnection GetConnection() => new SqlConnection(_connectionString);

    public List<Movie> GetInitialMovies()
    {
        List <Movie> movies = [];
        using SqlConnection connection = GetConnection();
        try
        {
            connection.Open();

            string selectQuery = "SELECT * FROM Movies";
            SqlCommand selectCommand = new(selectQuery, connection);

            SqlDataReader reader = selectCommand.ExecuteReader();

            while (reader.Read())
            {
                var movie = new Movie
                (
                    (int)reader["Id"],
                    (string)reader["Title"],
                    (string)reader["Director"],
                    (string)reader["Genre"],
                    (int)reader["ReleaseYear"],
                    (decimal)reader["Price"]
                );

                movies.Add(movie);
            }
            reader.Close();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }

        return movies;
    }
}