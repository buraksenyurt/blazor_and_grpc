using Microsoft.EntityFrameworkCore;
using MusicLibrary.Server;
using MusicLibrary.Server.Data;
using MusicLibrary.Server.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

/*
    SQL Server kullanıyoruz ve DbContext nesnesini middleware'e eklerken 
    appsettings'te dev_conn ile belirtilen connection string bilgisini kullanıyoruz.
*/
builder.Services.AddDbContext<MusicLibraryDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("dev_conn"));

#if DEBUG
    options.EnableDetailedErrors();
    options.EnableSensitiveDataLogging();
#endif
});
// AutoMapper hizmetini ilave ederken özel eşleştirme işleri için SmaryMapper'ı kullanacağını belirtiyoruz.
builder.Services.AddAutoMapper(typeof(SmartMapper));

// Album ve Müzisyneler için ilgili veritabanı işlemlerini üstlenen servisler DI çalışma ortamına eklenir.
builder.Services.AddTransient<AlbumService>();
builder.Services.AddTransient<MusicianService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
