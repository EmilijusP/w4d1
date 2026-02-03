using AnagramSolver.BusinessLogic.Data;
using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var settings = builder.Configuration.GetSection("Settings").Get<AppSettings>();
builder.Services.AddScoped<IWordRepository>(sp => new FileWordRepository(settings.FilePath));
builder.Services.AddScoped<IWordProcessor, WordProcessor>();
builder.Services.AddScoped<IAnagramDictionaryService, AnagramDictionaryService>();
builder.Services.AddScoped<IAnagramAlgorithm, AnagramAlgorithm>();
builder.Services.AddScoped<IAnagramSolver>
    (sp => new AnagramSolverService
        (
            sp.GetRequiredService<IWordProcessor>(), 
            sp.GetRequiredService<IAnagramDictionaryService>(),
            sp.GetRequiredService<IAnagramAlgorithm>(),
            sp.GetRequiredService<IWordRepository>(),
            settings.AnagramCount,
            settings.MinOutputWordsLength
        )
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
