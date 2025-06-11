using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium2.Models;

[Table("Character_Title")]
[PrimaryKey(nameof(CharacterId), nameof(TitleId))]
public class CharacterTitle
{
    [ForeignKey(nameof(Character))]
    public int CharacterId { get; set; }
    public Character Character { get; set; } = null!;

    [ForeignKey(nameof(Title))]
    public int TitleId { get; set; }
    public Title Title { get; set; } = null!;

    public DateTime AcquiredAt { get; set; }
}