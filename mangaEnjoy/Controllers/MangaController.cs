using EntityFrameworkCore.UnitOfWork;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using mangaEnjoy.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace mangaEnjoy.Controllers;

public class MangaController : ControllerBase
{
    private readonly IMangaService mangaService;
    private readonly IUnitOfWork unitOfWork;

    public MangaController(IMangaService mangaService, IUnitOfWork unitOfWork)
    {
        this.mangaService = mangaService;
        this.unitOfWork = unitOfWork;
    }

    [HttpPost("{apiId}")]
    public async Task<ActionResult> AddManga([FromRoute] string apiId, [FromQuery] Guid userId)
    {
        var mangaId = await mangaService.AddManga(apiId, userId);
        await unitOfWork.SaveChangesAsync();
        return Ok(mangaId);
    }
}