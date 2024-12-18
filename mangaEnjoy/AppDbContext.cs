using mangaEnjoy.DbTablesConfiguration;
using mangaEnjoy.Entities;
using Microsoft.EntityFrameworkCore;

namespace mangaEnjoy;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<MangaEntity> Mangas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}

