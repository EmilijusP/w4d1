using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.Contracts.Models;
using AnagramSolver.BusinessLogic.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var settings = builder.Configuration.GetSection("Settings").Get<AppSettings>();
builder.Services.AddScoped<IWordProcessor, WordProcessor>();
builder.Services.AddScoped<IAnagramDictionaryService, AnagramDictionaryService>();
builder.Services.AddScoped<IAnagramAlgorithm, AnagramAlgorithm>();
builder.Services.AddScoped<IWordRepository>(sp => new FileWordRepository(settings.FilePath));
builder.Services.AddScoped<IInputValidation, InputValidation>();
builder.Services.AddScoped<IAnagramSolver, AnagramSolverService>
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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
