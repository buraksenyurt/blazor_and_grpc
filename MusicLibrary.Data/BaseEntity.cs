using System.ComponentModel.DataAnnotations;

namespace MusicLibrary.Data.Entity;

/*
    Bazı Entity nesnelerinin Id ve Name alanı olması beklenmektedir.
    Bu nedenle bir base class tanımı söz konusudur.
    Album ve Musician bu entity nesnelerindendir ancak albümler ile müzisyneler arasındaki many-to-many ilişkiyi 
    temsil eden AlbumMusician için bu gerekli değildir. İlişkisel bütünlüğü ifade eden bir ara tablodur en nihayetinde.
*/
public class BaseEntity
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}