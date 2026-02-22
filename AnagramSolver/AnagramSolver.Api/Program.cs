using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.Contracts.Models;
using AnagramSolver.BusinessLogic.Data;
using AnagramSolver.Api.GraphQL;
using Microsoft.Extensions.Options;
using AnagramSolver.BusinessLogic.Factories;
using AnagramSolver.BusinessLogic.Filters;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var settings = builder.Configuration.GetSection("Settings").Get<AppSettings>();
builder.Services.AddSingleton<IAppSettings>(settings);
builder.Services.AddSingleton<IMemoryCache<IEnumerable<string>>, MemoryCache<IEnumerable<string>>>();
builder.Services.AddScoped<IWordProcessor, WordProcessor>();
builder.Services.AddScoped<IAnagramDictionaryService, AnagramDictionaryService>();
builder.Services.AddScoped<IComplexAnagramAlgorithm, ComplexAnagramAlgorithm>();
builder.Services.AddScoped<IAnagramSolverAlgorithm, SimpleAnagramAlgorithm>();
builder.Services.AddScoped<IAnagramAlgorithmFactory, AnagramAlgorithmFactory>();
builder.Services.AddDbContext<AnagramDbContext>();
builder.Services.AddScoped<IWordRepository, EfWordRepository>();
builder.Services.AddScoped<ISearchLogRepository, EfWordRepository>();
builder.Services.AddScoped<IInputValidation, InputValidation>();
builder.Services.AddTransient<IAnagramFilter, OutputLengthFilter>();
builder.Services.AddTransient<IAnagramFilter, CharacterFitFilter>();
builder.Services.AddScoped<IFilterPipeline, FilterPipeline>();
builder.Services.AddScoped<IAnagramSolver, AnagramSolverService>();

builder.Services.AddGraphQLServer().AddQueryType<Query>();

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

app.MapGraphQL();

app.Run();
