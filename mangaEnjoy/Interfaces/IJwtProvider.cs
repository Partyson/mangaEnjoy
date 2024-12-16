using mangaEnjoy.Entities;

namespace mangaEnjoy.Interfaces;

public interface IJwtProvider
{
    string GenerateToken(UserEntity user);
}