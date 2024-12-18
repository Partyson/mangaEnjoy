using mangaEnjoy.dto;
using mangaEnjoy.Entities;
using mangaEnjoy.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace mangaEnjoy.Services;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;

    public UserService(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<UserResponseDto?> GetUserProfile(Guid userId)
    {
        var userEntity = await GetUserEntityById(userId);
        return userEntity == null ? null : userEntity.Adapt<UserResponseDto>();
    }

    public async Task<UserEntity?> GetUserEntityById(Guid userId)
    {
        var query = userRepository.SingleResultQuery()
            .Include(x => 
                x.Include(u => u.AddedManga))
            .AndFilter(x => !x.IsDeleted)
            .AndFilter(x => x.Id == userId);
        var userEntity = await userRepository.FirstOrDefaultAsync(query);
        return userEntity;
    }
}