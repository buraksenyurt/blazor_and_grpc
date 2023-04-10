using Microsoft.AspNetCore.Mvc;
using MusicLibrary.Data.Entity;
using MusicLibrary.Server.Service;
using MusicLibrary.Server.Shared.Model;

namespace MusicLibrary.Server.Controllers;

/*
    Ortak işlemleri bünyesinde toplayan generic controller sınıfı.
    Model, Entity ve Service tipleri için kıstaslar da söz konusu.

    Service nesnesi constructor üzerinden enjekte edilir.
*/
public class BaseController<M, E, S> :
    ControllerBase
    where E : BaseEntity
    where M : IModel, new()
    where S : BaseService<E, M>
{
    private readonly S _service;
    private readonly string _createPath;

    public BaseController(S service, string createPath)
    {
        _service = service;
        _createPath = createPath;
    }

    /*
        HTTP Get ile çalışan, URL'den integer id parametresi alan fonksiyon.
        Enjekte edilen servisin GetByIdAsync fonksiyonunu kullanarak istenen modeli geriye döner.
    */
    [HttpGet("{id:int}")]
    public virtual async Task<M> Get(int id)
    {
        return await _service.GetByIdAsync(id);
    }

    /*    
        Enjekte edilmiş servisi kullanarak istenen veri kaynağına(model'den hareketle) ait listeyi döndürür.
        HTTP Get operasyonudur.
        Sayfa ve sayfada gösterilecek kayıt sayısını URL yolundan alınır.        
    */
    [HttpGet("list/{page:int}/{count:int}")]
    public virtual async Task<IEnumerable<M>> Get(int page, int count)
    {
        return await _service.GetAllAsync(page, count);
    }

    /*
        Yeni bir model eklemek için kullanılan HTTP Post fonksiyonu.
        Parametre olarak mesaj gövdesinden(body) JSON formatlı model içeriği alı.
        Enjekte edilen servis sınıfını kullanarak modelin veri tabanına işlenmesi sağlanır.
        Oluşan yeni veri yeni bir Id değerine sahip olacaktır.
        Geriye eklenen kaynağa(resource) ulaşmak için gerekli URL bilgisi döndürülür.
        Create fonksiyonu istemci için HTTP 201 mesajını üretir.
    */
    [HttpPost]
    public virtual async Task<IActionResult> Create([FromBody] M model)
    {
        model = await _service.CreateAsync(model);
        return Created($"{_createPath}/{model.Id}", new { Id = model.Id });
    }

    /*
        Bir veri kaynağını güncellemek için kullanılır.
        HTTP Put ile çalışır. 
        Güncellenmek istenen kaynağın id bilgisi URL yolundan(Route) çekilir. [FromRoute]
        Veri içeriği ise mesaj gövdesinden alınır. [FromBody]
    */
    [HttpPut]
    public virtual async Task<IActionResult> Update([FromRoute] int id, [FromBody] M model)
    {
        await _service.UpdateAsync(id, model);
        return Ok();
    }

    /*
        Bir veriyi silmek için kullanılan operasyondur.
        HTTP Delete mesajı ile çalışır.
        HTTP 204 NoContent mesajı döner.
    */
    [HttpDelete("{id:int}")]
    public virtual async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}