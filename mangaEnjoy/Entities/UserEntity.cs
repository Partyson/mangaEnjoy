using signa.Entities;

namespace mangaEnjoy.Entities;

public class UserEntity : BaseEntity
{
    public string Login  { get; set; }

    public string Username { get; set; }
    
    public string Email { get; set; }
    
    public string? PhoneNumber { get; set; }
    
    public string PasswordHash { get; set; }
    
    public string PasswordSalt { get; set; }

    public bool IsVerified { get; set; }

    public bool IsDeleted { get; set; }

    public List<MangaEntity> AddedManga { get; set; } = [];
}