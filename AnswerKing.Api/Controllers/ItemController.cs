using AnswerKing.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using AnswerKing.Infrastructure.Services;
using AnswerKing.Infrastructure.Repository;

namespace AnswerKing.Api.Controllers
{
    [ApiController]
    [Route("api/items")]

    public class ItemController : Controller
    {
        private IMemory<IEntity> db = InMemoryDatabase.AcquireDatabase();
        private readonly IItemRepository itemRepository;

        public ItemController(IItemRepository _itemRepository)
        {
            itemRepository = _itemRepository;
        }
        
        [HttpPost]
        [Route("{itemName}")]
        public IActionResult Create(string itemName)
        {
            var item = this.itemRepository.Create(itemName);
            return Ok(item);
        }

        [HttpGet]
        [Route("{itemId}")]
        public IActionResult GetItem(long itemId)
        {
            var item = db.GetById<Item>(itemId);

            if (item == null)
                return BadRequest("No item with this id!");

            return Ok(item);
        }

        [HttpGet]
        public IActionResult GetAllItems()
        {
            var items = this.itemRepository.GetAll();

            if (items.Count == 0)
                return BadRequest("There are no items!");

            return Ok(items);
        }

        [HttpDelete]
        [Route("{itemId}")]
        public IActionResult DeleteItem(long itemId)
        {
            var item = this.itemRepository.Delete(itemId);

            if (item == null)
                return BadRequest("Item not found!");

            return Ok(item);
        }

        [HttpPut]
        [Route("{itemId}/{newName}")]
        public IActionResult Update(long itemId, string newName)
        {
            var item = this.itemRepository.Get(itemId);

            if (item == null)
                return NotFound("No item with this Id");

            item.Name = newName;
            this.itemRepository.Update(item);

            return Ok(item);
        }
    }
}
