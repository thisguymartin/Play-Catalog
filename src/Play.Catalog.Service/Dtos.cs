using System;
using System.ComponentModel.DataAnnotations;
namespace Play.Catalog.Dtos
{
    public record ItemDto(Guid Id, string Name, string Description, decimal Price, DateTimeOffset CreatedDate);

    public record CreateItemDto([Required] string Name, [Required] string Description, [Required] decimal Price);

    public record UpdatedItemDto(string Name, string Description, decimal Price);

}