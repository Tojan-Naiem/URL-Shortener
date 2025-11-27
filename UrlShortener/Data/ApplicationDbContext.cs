using Microsoft.EntityFrameworkCore;
using UrlShortener.Models;
using UrlShortener.Services;
namespace UrlShortener.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ShortenedUrl> ShortenedUrls { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected ApplicationDbContext()
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShortenedUrl>(builder =>
            {
                builder.Property(s => s.Code).HasMaxLength(UrlShorteningService.NumberOfCharsInShortLink);
                builder.HasIndex(s => s.Code).IsUnique();
            });
        }
    }
}
