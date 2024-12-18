namespace mangaEnjoy.Interfaces;

public interface IMangaService
{
    Task<Guid> AddManga(string apiId, Guid userId);
}