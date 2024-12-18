using signa.Entities;

namespace mangaEnjoy.Entities;

public class MangaEntity : BaseEntity
{
    public string ApiId { get; set; }

    public List<UserEntity> UsersWhoAdd { get; set; } = [];
}