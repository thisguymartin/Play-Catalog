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
      if (item == null)
      {
        return NotFound();
      }
      return Ok(item);
    }

    [HttpPost]
    public ActionResult<ItemDto> Post(CreateItemDto createItemDto)
    {
      var item = new ItemDto(Guid.NewGuid(), createItemDto.Name, createItemDto.Description, createItemDto.Price, DateTimeOffset.Now);
      items.Add(item);
      return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public ActionResult<ItemDto> Update(Guid id, UpdatedItemDto item)
    {
      var existingItem = items.Where(x => x.Id == id).SingleOrDefault();
      if (existingItem == null)
      {
        return NotFound();
      }

      var updateItem = existingItem with
      {
        Name = item.Name,
        Description = item.Description,
        Price = item.Price
      };

      var index = items.FindIndex(existingItem => existingItem.Id == id);
      items[index] = updateItem;

      return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult<ItemDto> Delete(Guid id)
    {
      var index = items.FindIndex(existingItem => existingItem.Id == id);
      if (index < 0)
      {
        return NotFound();
      }
      
      items.RemoveAt(index);

      return NoContent();
    }



  }

}