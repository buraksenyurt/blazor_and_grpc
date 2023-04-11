namespace MusicLibrary.Shared;

/*
    Servis tarafında kullanılacak olan model nesneleri 
    otomatik olarak Id ve Name özelliklerini uyarlamalıdır.
*/
public interface IModel
{
    int Id { get; set; }
    string Name { get; set; }
}