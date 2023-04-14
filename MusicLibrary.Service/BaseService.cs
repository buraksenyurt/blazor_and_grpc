using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MusicLibrary.Data.Entity;
using MusicLibrary.Shared.Model;

namespace MusicLibrary.Service;

/*
    Temel CRUD operasyonları ile ilgili veritabanı iletişimini sağlayan generic sınıf.
    E, Entity ve M, Model anlamında.
    Entity'ler BaseEntity'den türemiş olmalılar. Model'ler ise IModel interface'ini
    uyarlamış olmalılar ve default constructor'ları bulunmalı.

    Ayrıca bu sınıf abstract olarak tanımlanmıştır. Yani kendisi nesne olarak örneklenemez.
    Çalışma zamanına kayıt edilecek(register) servisler bu sınıftan türerler.
*/
public abstract class BaseService<E, M>
    where E : BaseEntity
    where M : IModel, new()
{
    private readonly MusicLibraryDbContext _dbContenxt;
    private readonly IMapper _mapper;

    /*
        Serviste kullanılacak bağımlılıklar constructor üzerinden enjekte edilirler.
    */
    public BaseService(MusicLibraryDbContext dbContext, IMapper mapper)
    {
        _dbContenxt = dbContext;
        _mapper = mapper;
    }

    /*
        Önyüz servisinden gelecek çağrılarda Create operasyonları için devreye girecek fonksiyon.
    */
    public async Task<M> CreateAsync(M model)
    {
        // Model nesnesinden entity nesnesine dönüştür
        var entity = _mapper.Map<E>(model);
        // Context'e entity nesne örneğini ekle
        await _dbContenxt.Set<E>().AddAsync(entity);
        // Değişiklikleri kaydet
        await _dbContenxt.SaveChangesAsync();
        // entity'den model nesnesine dönüştür ve geri gönder(Üretilen otomatik ID'lerle doldurulmuş olacaktır)
        return _mapper.Map<M>(entity);
    }

    // Delete ve Update işlemleri için kullanılacak yardımcı metot.
    // Bunu dışarıya açmadığımız için private
    private async Task<E> GetEntityAsync(int id)
    {        
        var entity = await _dbContenxt.FindAsync<E>(id);
        if (entity == null)
        {
            throw new Exception($"{id} değerine sahip {typeof(E)} bulunamadı.");
        }
        return entity;
    }

    // ID bazlı entity aramaları için dışarıya açılan fonksiyon
    public async Task<M> GetByIdAsync(int id)
    {
        var entity = await GetEntityAsync(id);
        return _mapper.Map<M>(entity);
    }

    /*
        Paging prensiplerine göre bir entity listesini döndüren fonksiyon.
        Tüm listeyi döndürmek veri büyüdükçe iyi bir pratik olmayabilir.
    */
    public async Task<IEnumerable<M>> GetAllAsync(int page, int count)
    {
        /*
            AsNoTracking var çünkü; Liste çekle işleminden veri değişikliği beklemediğimizden,
            context'in değişiklikleri izlemesine gerek yok. Bu kullanım, fazla bellek tüketimini de önler.
        */
        var entities = await _dbContenxt.Set<E>()
            .AsNoTracking()
            .Select(e => e)
            .Skip((page - 1) * count)
            .Take(count).ToListAsync();

        // Entity'lerin her biri Model'e çevrilerek geri döndürülür.
        return entities.Select(e => _mapper.Map<M>(e));

    }

    // Veri güncelleme fonksiyonu
    public async Task<M> UpdateAsync(int id, M model)
    {
        // Önce ilgili entity bulunur
        var entity = await GetEntityAsync(id);
        // parametre olarak gelen model entity'ye çevrilir. Dolayısıyla güncellenen değerler taşınmış olur
        _mapper.Map<M, E>(model, entity);
        // Veri değişikliği gerçekleştirilir
        await _dbContenxt.SaveChangesAsync();
        // entity son hali ile tekrardan model'e dönüştürülür ve geri döndürülür.
        return _mapper.Map<M>(entity);
    }

    // Veri silme fonksiyonu
    public async Task DeleteAsync(int id)
    {
        // id ile gelen entity aranır
        var entity = await GetEntityAsync(id);
        // context'ten çıkarılır
        _dbContenxt.Set<E>().Remove(entity);
        // değişiklikler kaydedilir
        await _dbContenxt.SaveChangesAsync();
    }
}