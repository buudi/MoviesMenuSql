namespace MoviesMenuSql.Models;

public class Movie(int? Id, string? Title, string? Director, string? Genre, int? ReleaseYear, double? Price)
{

    public int? Id { get; set; } = Id;
    public string? Title { get; set; } = Title;
    public string? Director { get; set; } = Director;
    public int? ReleaseYear { get; set; } = ReleaseYear;
    public string? Genre { get; set; } = Genre;
    public double? Price { get; set; } = Price;

}
