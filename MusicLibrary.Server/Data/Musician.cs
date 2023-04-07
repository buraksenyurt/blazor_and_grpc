namespace MusicLibrary.Server.Data;

public class Musician
    : BaseEntity
{
    public string Bio { get; set; } = string.Empty;
    public int StarPoint { get; set; }
    public DateTime Birthdate { get; set; }
    public List<AlbumMusician> Albums { get; set; } = new List<AlbumMusician>();
}