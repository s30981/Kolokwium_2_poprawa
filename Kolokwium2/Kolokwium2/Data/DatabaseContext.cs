using Kolokwium2.Models;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium2.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Character> Characters { get; set; } 
    public DbSet<Item> Items  { get; set; } 
    public DbSet<Backpack> Backpacks { get; set; } 
    public DbSet<Title> Titles { get; set; } 
    public DbSet<CharacterTitle> CharacterTitles { get; set; } 

    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder m)
    {
        m.Entity<Character>().HasData(
            new Character { CharacterId = 1, FirstName = "John",  LastName = "Yakuza", CurrentWeight = 43, MaxWeight = 200 },
            new Character { CharacterId = 2, FirstName = "John",  LastName = "Yakuza2", CurrentWeight =  0, MaxWeight = 150 }
        );

        m.Entity<Item>().HasData(
            new Item { ItemId = 1, Name = "Item1", Weight = 10 },
            new Item { ItemId = 2, Name = "Item2", Weight = 11 },
            new Item { ItemId = 3, Name = "Item3", Weight = 12 }
        );

        m.Entity<Title>().HasData(
            new Title { TitleId = 1, Name = "Title1" },
            new Title { TitleId = 2, Name = "Title2" },
            new Title { TitleId = 3, Name = "Title3" }
        );

        m.Entity<Backpack>().HasData(
            new { CharacterId = 1, ItemId = 1, Amount = 2 },
            new { CharacterId = 1, ItemId = 2, Amount = 1 },
            new { CharacterId = 1, ItemId = 3, Amount = 1 }
        );

        m.Entity<CharacterTitle>().HasData(
            new { CharacterId = 1, TitleId = 1, AcquiredAt = DateTime.Parse("2025-06-10") },
            new { CharacterId = 1, TitleId = 2, AcquiredAt = DateTime.Parse("2025-06-09") },
            new { CharacterId = 1, TitleId = 3, AcquiredAt = DateTime.Parse("2025-06-08") }
        );
    }
}
