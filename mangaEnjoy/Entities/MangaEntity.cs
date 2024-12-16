using signa.Entities;

namespace mangaEnjoy.Entities;

public class MangaEntity : BaseEntity
{
    public Guid ApiId { get; set; }

    public List<UserEntity> UsersWhoAdd { get; set; } = [];
}