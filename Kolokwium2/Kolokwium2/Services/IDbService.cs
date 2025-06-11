using Kolokwium2.DTOs;

namespace Kolokwium2.Services;

public interface IDbService
{
    Task<CharacterDto> GetCharacterAsync(int id);
    Task AddItemsAsync(int characterId, IEnumerable<int> itemIds);
}