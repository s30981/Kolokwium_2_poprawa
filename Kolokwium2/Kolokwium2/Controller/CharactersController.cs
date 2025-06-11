using Kolokwium2.Exceptions;
using Kolokwium2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium2.Controller;

[ApiController]
[Route("api/characters")]
public class CharactersController : ControllerBase
{
    private readonly IDbService _dbService;
    public CharactersController(IDbService dbService) => _dbService = dbService;

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCharacter(int id)
    {
        try { return Ok(await _dbService.GetCharacterAsync(id)); }
        catch (NotFoundException e) { return NotFound(e.Message); }
    }

    [HttpPost("{id}/backpacks")]
    public async Task<IActionResult> AddItems(int id, [FromBody] List<int> itemIds)
    {
        if (itemIds is null || itemIds.Count == 0)
            return BadRequest("Body must be a non-empty");

        try
        {
            await _dbService.AddItemsAsync(id, itemIds);
            return StatusCode(201);
        }
        catch (NotFoundException e) { return NotFound(e.Message); }
        catch (ConflictException e) { return Conflict(e.Message); }
    }
}