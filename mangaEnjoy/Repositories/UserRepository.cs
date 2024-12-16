using EntityFrameworkCore.Repository;
using mangaEnjoy.Entities;
using mangaEnjoy.Interfaces;

namespace mangaEnjoy.Repositories;

public class UserRepository(AppDbContext context) : Repository<UserEntity>(context), IUserRepository;