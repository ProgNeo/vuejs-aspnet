using Anime.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AnimeContext>(
    options =>
    {
        options.UseMySql(builder.Configuration.GetConnectionString("AnimeDB"), ServerVersion.Parse("8.0.31-0ubuntu0.22.04.1"));
    });

var app = builder.Build();

app.UseCors(options =>
    options.WithOrigins("http://localhost:5173")
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
);
app.UseCors(options =>
    options.WithOrigins("http://localhost:5174")
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
);

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