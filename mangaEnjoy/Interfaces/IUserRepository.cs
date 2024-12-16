using EntityFrameworkCore.Repository.Interfaces;
using mangaEnjoy.Entities;

namespace mangaEnjoy.Interfaces;

public interface IUserRepository : IRepository<UserEntity>
{
    
}