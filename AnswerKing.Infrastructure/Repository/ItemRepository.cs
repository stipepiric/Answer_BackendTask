using AnswerKing.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using AnswerKing.Core.Entities;
using System.Linq;

namespace AnswerKing.Infrastructure.Repository
{
    public class ItemRepository : IItemRepository
    {
        IMemory<IEntity> db = InMemoryDatabase.AcquireDatabase();
        public ItemRepository()
        {
            this.Collection = db.GetAll<Item>();
        }

        private IList<Item> Collection { get; set; }

        public IList<Item> GetAll()
        {
            return this.Collection;
        }

        public Item Get(long id)
        {
            var item = this.Collection.Where(i => i.Id == id).FirstOrDefault();

            return item;
        }

        public Item Create(string itemName)
        {
            var item = db.CreateEntity<Item>();
            item.Name = itemName;
            item.Categories = new List<Category>();
            db.AddEntity(item);

            return item;
        }

        public Item Update(Item item)
        {
            if(item == null)
            {
                throw new ArgumentNullException();
            }

            var existing = this.Collection.SingleOrDefault(p => p.Id == item.Id);

            if (existing != null)
            {
                this.Collection = this.Collection.Except(new List<Item> { existing }).ToList();
                this.Collection.Add(item);

                existing = item;
            }

            return existing;
        }
        public Item Delete(long itemId)
        {
            var item = this.Collection.Where(i => i.Id == itemId).FirstOrDefault();

            if(item == null)
            {
                throw new ArgumentNullException();
            }

            db.Remove(item);

            return item;
        }


    }
}
