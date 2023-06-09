namespace MusicLibrary.Shared.Model;

public class AlbumModel : IModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public CategoryModel Category { get; set; }
    public int Year { get; set; }
    public int[] MusicianIds { get; set; } = Array.Empty<int>();
}