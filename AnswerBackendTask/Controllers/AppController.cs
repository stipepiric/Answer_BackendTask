using AnswerBackendTask.Entities;
using Microsoft.AspNetCore.Mvc;
using AnswerBackendTask.Services;
using System.Diagnostics.Contracts;

namespace AnswerBackendTask.Controllers
{
    public class AppController : Controller
    {
        private readonly InMemoryDatabase db;
        
        public AppController()
        {
            this.db = new InMemoryDatabase(new InMemoryDatabase.Storage());
        }
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
    }
}
