using mangaEnjoy.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace mangaEnjoy.DbTablesConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(u => u.AddedManga)
            .WithMany(m => m.UsersWhoAdd);
    }
}