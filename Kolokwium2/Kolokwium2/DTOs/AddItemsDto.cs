using System.ComponentModel.DataAnnotations;

namespace Kolokwium2.DTOs;

public class AddItemsDto
{
    [Required, MinLength(1, ErrorMessage = "Provide at least one itemId")]
    public List<int> ItemIds { get; set; } = [];
}