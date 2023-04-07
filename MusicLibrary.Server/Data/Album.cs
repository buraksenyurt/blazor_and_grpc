namespace MusicLibrary.Server.Data;

public class Album
    : BaseEntity
{
    public int Year { get; set; }
    public Category Category { get; set; }
    public List<AlbumMusician> Musicians { get; set; } = new List<AlbumMusician>();
}