namespace MusicLibrary.Server.Data;

public class AlbumMusician
{
    public int AlbumId { get; set; }
    public Album Album { get; set; } = null!;
    public int MusicianId { get; set; }
    public Musician Musician { get; set; } = null!;
}