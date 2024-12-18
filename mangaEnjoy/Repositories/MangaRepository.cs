using EntityFrameworkCore.Repository;
using mangaEnjoy.Entities;
using mangaEnjoy.Interfaces;

namespace mangaEnjoy.Repositories;

public class MangaRepository(AppDbContext context) : Repository<MangaEntity>(context), IMangaRepository;