using System.Data.SqlClient;

namespace MoviesMenuSql.Services;

public class DbService
{
    private readonly string _connectionString;
    public DbService() => _connectionString = "Server=localhost\\SQLEXPRESS;Database=MyBootcamp;Trusted_Connection=True;";
    public SqlConnection GetConnection() => new SqlConnection(_connectionString);
}