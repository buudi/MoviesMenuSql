using MoviesMenuSql.Models;

namespace MoviesMenuSql.Services;

internal class MovieService
{
    List<Movie> movies;
    public MovieService()
    {
        movies = [
            new Movie(1, "Inception", "Christopher Nolan", "Sci-fi/Action", 2010, 24.99),
            new Movie(2, "The Matrix", "Lilly Wachowski", "Sci-fi/Action", 1999, 19.99),
            new Movie(3, "Inside Job", "Charles Ferguson", "Crime", 2010, 12.49)
         ];
    }

    public string ListAllMovies()
    {
        if (!movies.Any())
        {
            return "No movies available.";
        }

        var result = from movie in movies
                     select $"ID: {movie.Id}, Title: {movie.Title}, Director: {movie.Director}, Genre: {movie.Genre}, Year: {movie.ReleaseYear}, Price: {movie.Price}";

        return "Movies:\n\n" + string.Join("\n", result);
    }

    public string AddMovie(Movie movie)
    {
        movie.Id = movies.Count + 1;
        movies.Add(movie);
        return "Movie added successfully.";
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
