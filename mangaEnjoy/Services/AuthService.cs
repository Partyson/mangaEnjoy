using mangaEnjoy.dto;
using mangaEnjoy.Entities;
using mangaEnjoy.Helpers;
using mangaEnjoy.Interfaces;
using Mapster;

namespace mangaEnjoy.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository userRepository;
    private readonly IJwtProvider jwtProvider;


    public AuthService(IUserRepository userRepository, IJwtProvider jwtProvider)
    {
        this.userRepository = userRepository;
        this.jwtProvider = jwtProvider;
    }

    public async Task<string?> RegisterUser(RegisterUserDto newUser)
    {
        var newUserEntity = newUser.Adapt<UserEntity>();
        var addedUser = await userRepository.AddAsync(newUserEntity);
        return jwtProvider.GenerateToken(addedUser);
    }

    public async Task<string> LoginUser(string login, string password)
    {
        var query = userRepository.SingleResultQuery()
            .AndFilter(x => !x.IsDeleted)
            .AndFilter(x => x.Login == login);
        var userEntity = await userRepository.FirstOrDefaultAsync(query);
        
        var passwordIsCorrect = PasswordHasher.VerifyPassword(password, userEntity.PasswordHash,
            Convert.FromBase64String(userEntity.PasswordSalt));
        if(!passwordIsCorrect)
            throw new UnauthorizedAccessException();

        return jwtProvider.GenerateToken(userEntity);
    }
}