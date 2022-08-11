using System;
using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Dtos;

namespace Play.Catalog.Service.Controller
{

  [ApiController]
  [Route("items")]
  public class ItemsController : ControllerBase
  {

    private static readonly List<ItemDto> items = new()
      {
         new ItemDto(Guid.NewGuid(), "Potion", "Small Potion", 522, DateTimeOffset.UtcNow),
         new ItemDto(Guid.NewGuid(), "Sword", "Steel Sword", 523, DateTimeOffset.UtcNow)
      };


    [HttpGet]
    public IEnumerable<ItemDto> Get()
    {
      return items;
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
      var item = items.Where(x => x.Id == id).SingleOrDefault();
      return Ok(item);
    }


  }


}