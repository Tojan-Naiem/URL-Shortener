using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using UrlShortener.Data;
using UrlShortener.Models;
using UrlShortener.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")

    ));
builder.Services.AddScoped<UrlShorteningService>();
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var configuration = builder.Configuration.GetConnectionString("Redis");
    return ConnectionMultiplexer.Connect(configuration);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   
}

app.UseHttpsRedirection();

app.MapPost("api/shorten", async (
    ShortenUrlRequest request,
    UrlShorteningService urlShorteningService,
    ApplicationDbContext dbContext,
    HttpContext httpContext
    ) =>
{
    if (!Uri.TryCreate(request.Url,UriKind.Absolute,out _))
    {
        return Results.BadRequest("The specified URL is invalid");
    }
    var code = await urlShorteningService.GenerateUniqueCode();
    var shortenedUrl = new ShortenedUrl
    {
        Id = Guid.NewGuid(),
        LongUrl = request.Url,
        Code = code,
        ShortUrl =$"{httpContext.Request.Scheme}://{httpContext.Request.Host}/api/{code}",
        CreatedOnUtc=DateTime.Now
    };
    await dbContext.ShortenedUrls.AddAsync(shortenedUrl);
    await dbContext.SaveChangesAsync();
    return Results.Ok(shortenedUrl.ShortUrl);

});


app.MapGet("api/{code}", async (
    string code,
    ApplicationDbContext dbContext
    ) =>
{
    var shortenedUrl = await dbContext.ShortenedUrls
                             .FirstOrDefaultAsync(s => s.Code == code);
    if (shortenedUrl is null) return Results.NotFound();
    return Results.Redirect(shortenedUrl.LongUrl);
});

app.UseAuthorization();

app.MapControllers();

app.Run();
