using mangaEnjoy.dto;

namespace mangaEnjoy.Interfaces;

public interface IAuthService
{
    Task<string?> RegisterUser(RegisterUserDto newUser);
    Task<string> LoginUser(string email, string password);
}