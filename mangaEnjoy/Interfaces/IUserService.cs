using mangaEnjoy.dto;
using mangaEnjoy.Entities;

namespace mangaEnjoy.Interfaces;

public interface IUserService
{
    Task<UserResponseDto?> GetUserProfile(Guid userId);
    public Task<UserEntity?> GetUserEntityById(Guid userId);
}