using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kolokwium2.Models;

[Table("Item")]
public class Item
{
    [Key] public int ItemId { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = null!;

    public int Weight { get; set; }

    public ICollection<Backpack> BackpackItems { get; set; } = null!;
}
