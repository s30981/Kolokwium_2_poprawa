using Kolokwium2.Data;
using Kolokwium2.DTOs;
using Kolokwium2.Exceptions;
using Kolokwium2.Models;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium2.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;
    public DbService(DatabaseContext context) => _context = context;

    public async Task<CharacterDto> GetCharacterAsync(int id)
    {
        var character = await _context.Characters
            .Where(c => c.CharacterId == id)
            .Select(c => new CharacterDto
            {
                FirstName     = c.FirstName,
                LastName      = c.LastName,
                CurrentWeight = c.CurrentWeight,
                MaxWeight     = c.MaxWeight,
                BackpackItems = c.BackpackItems.Select(b => new BackpackItemDto
                {
                    ItemName   = b.Item.Name,
                    ItemWeight = b.Item.Weight,
                    Amount     = b.Amount
                }).ToList(),
                Titles = c.CharacterTitles.Select(t => new TitleDto
                {
                    Title      = t.Title.Name,
                    AcquiredAt = t.AcquiredAt
                }).ToList()
            })
            .FirstOrDefaultAsync();

        if (character is null)
            throw new NotFoundException("Character not found");

        return character;
    }

     public async Task AddItemsAsync(int characterId, IEnumerable<int> itemIds)
    {
        var ids = itemIds?.ToList() ?? new List<int>();
        if (ids.Count == 0)
            throw new BadRequestException("Body must be a non-empty");

        using var tx = await _context.Database.BeginTransactionAsync();
        try
        {
            var character = await _context.Characters
                .Include(c => c.BackpackItems)
                .FirstOrDefaultAsync(c => c.CharacterId == characterId);

            if (character is null)
                throw new NotFoundException("Character not found");

            var distinctIds = ids.Distinct().ToList();
            var items = await _context.Items
                .Where(i => distinctIds.Contains(i.ItemId))
                .ToListAsync();

            if (items.Count != distinctIds.Count)
                throw new NotFoundException("One or more items not found");

            int addedWeight = ids
                .Select(id => items.First(i => i.ItemId == id).Weight)
                .Sum();

            if (character.CurrentWeight + addedWeight > character.MaxWeight)
                throw new ConflictException("Character is overloaded");

            foreach (var id in ids)
            {
                var entry = await _context.Backpacks
                    .FirstOrDefaultAsync(b => b.CharacterId == characterId && b.ItemId == id);

                if (entry is null)
                {
                    _context.Backpacks.Add(new Backpack
                    {
                        CharacterId = characterId,
                        ItemId      = id,
                        Amount      = 1
                    });
                }
                else
                {
                    entry.Amount += 1;
                    _context.Backpacks.Update(entry);
                }
            }
            character.CurrentWeight += addedWeight;
            _context.Characters.Update(character);

            await _context.SaveChangesAsync();
            await tx.CommitAsync();
        }
        catch
        {
            await tx.RollbackAsync();
            throw;
        }
    }
}
