namespace Anime.Models;

public class AnimeObjectViewModel
{
    public string? Title { get; set; }
    public int? Genre { get; set; }
    public string? Description { get; set; }
    public string? Info { get; set; }
    public IFormFile Image { get; set; }
}