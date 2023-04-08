namespace MusicLibrary.Server.Shared.Model;

public class MusicianModel : IModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public int StarPoint { get; set; }
    public DateTime Birthdate { get; set; }
    public int[] AlbumIds { get; set; } = Array.Empty<int>();
}