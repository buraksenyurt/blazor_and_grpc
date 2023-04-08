using Microsoft.EntityFrameworkCore;
using MusicLibrary.Server;
using MusicLibrary.Server.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddDbContext<MusicLibraryDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("dev_conn"));

#if DEBUG
    options.EnableDetailedErrors();
    options.EnableSensitiveDataLogging();
#endif
});
builder.Services.AddAutoMapper(typeof(SmartMapper));

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
