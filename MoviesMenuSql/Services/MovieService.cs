using MoviesMenuSql.Models;
using System.Data.SqlClient;

namespace MoviesMenuSql.Services;

internal class MovieService
{
    private DbService dbService = new();
    private List<Movie> movies = [];

    public MovieService()
    {
        List<Movie> initialMovies = dbService.GetInitialMovies();
        foreach (Movie movie in initialMovies)
        {
            movies.Add(movie);
        }
    }

    public List<Movie> ListAllMovies ()
    {
        return movies;
    }

    public string AddMovie(Movie movie)
    {
        movie.Id = movies.Count + 1;
        movies.Add(movie);
        return "Movie added successfully.";
    }

    //public string AddMovie(Movie movie)
    //{
    //    using SqlConnection connection = dbService.GetConnection();
    //    try
    //    {

    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception();
    //    }

    //}

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
