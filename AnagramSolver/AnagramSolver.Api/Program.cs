using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.Contracts.Models;
using AnagramSolver.BusinessLogic.Data;
using AnagramSolver.Api.GraphQL;
using Microsoft.Extensions.Options;

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
builder.Services.AddScoped<IWordRepository, FileWordRepository>();
builder.Services.AddScoped<IInputValidation, InputValidation>();
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
