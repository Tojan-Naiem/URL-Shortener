using UrlShortener.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

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

});

app.UseAuthorization();

app.MapControllers();

app.Run();
