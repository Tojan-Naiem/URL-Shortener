using Microsoft.EntityFrameworkCore;
using UrlShortener.Data;
using UrlShortener.Models;
using UrlShortener.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")

    ));
builder.Services.AddScoped<UrlShorteningService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPost("api/shorten", (ShortenUrlRequest request) =>
{
    if (!Uri.TryCreate(request.Url,UriKind.Absolute,out _))
    {
        return Results.BadRequest("The specified URL is invalid");
    }
    return null;

});

app.UseAuthorization();

app.MapControllers();

app.Run();
