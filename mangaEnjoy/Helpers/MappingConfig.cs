using Mapster;
using JetBrains.Annotations;
using mangaEnjoy.dto;
using mangaEnjoy.Entities;

namespace mangaEnjoy.Helpers;

[UsedImplicitly]
public class MappingConfig
{
    public static void RegisterMappings()
    {
        var salt = PasswordHasher.GenerateSalt();
        TypeAdapterConfig<RegisterUserDto, UserEntity>
            .NewConfig()
            .Map(dest => dest.PasswordHash, src => PasswordHasher.HashPassword(src.Password, salt))
            .Map(dest => dest.PasswordSalt, src => Convert.ToBase64String(salt));
        TypeAdapterConfig<UserEntity, UserResponseDto>
            .NewConfig()
            .Map(dest => dest.Username, src => src.Username)
            .Map(dest => dest.AddedMangas, 
                src => src.AddedManga.Select(x => x.ApiId).ToList())
            .IgnoreNonMapped(true);
    }
}