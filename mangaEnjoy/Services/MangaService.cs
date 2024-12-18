using mangaEnjoy.Entities;
using mangaEnjoy.Interfaces;

namespace mangaEnjoy.Services;

public class MangaService : IMangaService
{
    private readonly IMangaRepository mangaRepository;
    private readonly IUserService userService;

    public MangaService(IMangaRepository mangaRepository, IUserService userService)
    {
        this.mangaRepository = mangaRepository;
        this.userService = userService;
    }

    public async Task<Guid> AddManga(string apiId, Guid userId)
    {
        var userEntity = await userService.GetUserEntityById(userId);
        if(userEntity == null)
            return Guid.Empty;
        var query = mangaRepository.SingleResultQuery()
            .AndFilter(x => x.ApiId == apiId);
        var manga = await mangaRepository.FirstOrDefaultAsync(query);
        if (manga != null)
        {
            manga.UsersWhoAdd.Add(userEntity);
            return manga.Id;
        }

        var addedManga = await mangaRepository
            .AddAsync(new MangaEntity { ApiId = apiId, UsersWhoAdd = [userEntity] });
        return addedManga.Id;
    }
}