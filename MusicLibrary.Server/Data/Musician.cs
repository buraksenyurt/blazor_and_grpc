namespace MusicLibrary.Server.Data;

public class Musician
    : BaseEntity
{
    public string Fullname { get; set; } = string.Empty;
    public DateTime Birthdate { get; set; }
}