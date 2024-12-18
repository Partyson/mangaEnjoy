using mangaEnjoy.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace mangaEnjoy.DbTablesConfiguration;

public class MangaConfiguration: IEntityTypeConfiguration<MangaEntity>
{
    public void Configure(EntityTypeBuilder<MangaEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(u => u.UsersWhoAdd)
            .WithMany(m => m.AddedManga);
    }
}