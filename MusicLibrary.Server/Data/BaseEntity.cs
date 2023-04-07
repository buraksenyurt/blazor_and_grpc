using System.ComponentModel.DataAnnotations;
namespace MusicLibrary.Server.Data;

public class BaseEntity
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}