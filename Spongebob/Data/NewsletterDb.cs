using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;
using Spongebob.Models;

namespace Spongebob.Data
{
    public class NewsletterDb : DbContext
    {
        public NewsletterDb(DbContextOptions options)
            : base(options) { }

        public DbSet<Newsletter> Newsletters { get; init; }

        // The Create method can be removed. DbContext configuration will be handled with AddDbContext method in Program.cs
        public static NewsletterDb Create(IMongoDatabase database) =>
            new(new DbContextOptionsBuilder<NewsletterDb>()
                .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
                .Options);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Newsletter>().ToCollection("newsletter-db");
        }
    }
}
