using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kolokwium2.Models;

[Table("Character")]
public class Character
{
    [Key] public int CharacterId { get; set; }

    [Required, MaxLength(50)]
    public string FirstName { get; set; } = null!;

    [Required, MaxLength(120)]
    public string LastName { get; set; } = null!;

    public int CurrentWeight { get; set; }
    public int MaxWeight { get; set; }

    public ICollection<Backpack> BackpackItems { get; set; } = null!;
    public ICollection<CharacterTitle> CharacterTitles { get; set; } = null!;
}