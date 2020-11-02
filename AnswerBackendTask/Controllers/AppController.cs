using AnswerBackendTask.Entities;
using Microsoft.AspNetCore.Mvc;
using AnswerBackendTask.Services;

namespace AnswerBackendTask.Controllers
{
    public class AppController : Controller
    {
        private InMemoryDatabase db = InMemoryDatabase.AcquireDatabase();
        
        [HttpPost]
        [Route("/create/{itemName}")]
        public ActionResult<Item> CreateItem(string itemName)
        {
            var item = db.CreateEntity<Item>();
            item.Name = itemName;
            db.AddEntity(item);

            return item;
        }

        [HttpGet]
        [Route("/get/{itemId}")]
        public ActionResult<Item> GetItem(long itemId)
        {
            var item = db.GetById<Item>(itemId);

            if (item == null)
                return BadRequest("No item with this id!");

            return item;
        }

        [HttpDelete]
        [Route("/delete/{itemId}")]
        public ActionResult DeleteItem(long itemId)
        {
            var item = db.GetById<Item>(itemId);
            if (item == null)
                return BadRequest("Item not found!");

            db.Remove(item);
            return Ok();
        }

        [HttpPut]
        [Route("edit/{itemId}/{newName}")]
        public ActionResult<Item> EditItem(long itemId, string newName)
        {
            var item = db.GetById<Item>(itemId);

            if (item == null)
                return NotFound("No item with this Id");

            db.Remove(item);
            item.Name = newName;
            db.AddEntity(item);

            return item;
        }
    }
}
