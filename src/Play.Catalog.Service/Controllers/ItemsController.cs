using System;
using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Dtos;
using Play.Catalog.Service.Repository;
using System.Threading.Tasks;
using Play.Catalog.Service.Entities;

namespace Play.Catalog.Service.Controller
{

    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {

        private readonly IItemRepository itemRepository;

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetAsync()
        {
            var items = (await itemRepository.GetAllASync()).Select(item => item.AsDto());
            return items;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var item = (await itemRepository.GetAsync(id));
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> PostAsync(CreateItemDto createItemDto)
        {
            var item = new Item
            {
                Name = createItemDto.Name,
                Description = createItemDto.Description,
                Price = createItemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };


            await itemRepository.CreateAsync(item);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ItemDto>> UpdateAsync(Guid id, UpdatedItemDto item)
        {


            var existingItem = await itemRepository.GetAsync(id);
            if (existingItem == null)
            {
                return NotFound();
            }


            existingItem.Name = item.Name;
            existingItem.Price = item.Price;
            existingItem.Description = item.Description;

            await itemRepository.UpdateAsync(existingItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ItemDto>> DeleteAsync(Guid id)
        {

            var existingItem = await itemRepository.GetAsync(id);
            if (existingItem == null)
            {
                return NotFound();
            }

            await itemRepository.DeleteOneAsync(existingItem.Id);

            return NoContent();
        }



    }

}