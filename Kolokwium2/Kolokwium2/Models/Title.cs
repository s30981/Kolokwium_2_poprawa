using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kolokwium2.Models;

[Table("Title")]
public class Title
{
    [Key] public int TitleId { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = null!;

    public ICollection<CharacterTitle> CharacterTitles { get; set; } = null!;
}