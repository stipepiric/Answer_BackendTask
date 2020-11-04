using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using AnswerKing.Core.Entities;
using AnswerKing.Infrastructure.Services;

namespace AnswerKing.Infrastructure.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        IMemory<IEntity> db = InMemoryDatabase.AcquireDatabase();
        public CategoryRepository()
        {
            this.Collection = db.GetAll<Category>();
        }

        private IList<Category> Collection { get; set; }

        public IList<Category> GetAll()
        {
            return this.Collection;
        }

        public Category Create(string categoryName)
        {
            var category = db.CreateEntity<Category>();
            category.Name = categoryName;
            db.AddEntity(category);

            return category;
        }

        public Item AddItemToCategory(long itemId, long categoryId)
        {
            var item = this.db.GetAll<Item>().Where(i => i.Id == itemId).FirstOrDefault();

            if(item == null)
            {
                throw new ArgumentNullException();
            }
            db.Remove(item);
            var category = this.Collection.Where(c => c.Id == categoryId).FirstOrDefault();

            if(category == null)
            {
                throw new ArgumentNullException();
            }
            item.Categories.Add(category);
            db.AddEntity(item);

            return item;
        }

        public Item RemoveItemFromCategory(long itemId, long categoryId)
        {
            var item = this.db.GetAll<Item>().Where(i => i.Id == itemId).FirstOrDefault();

            if (item == null)
            {
                throw new ArgumentNullException();
            }
            db.Remove(item);
            var category = this.Collection.Where(c => c.Id == categoryId).FirstOrDefault();

            if (category == null)
            {
                throw new ArgumentNullException();
            }
            item.Categories.Remove(category);
            db.AddEntity(item);

            return item;
        }
        public Category Edit(long categoryId, string categoryName)
        {
            var category = this.Collection.Where(c => c.Id == categoryId).FirstOrDefault();
            
            if(category == null)
            {
                throw new ArgumentNullException();
            }

            this.Collection.Remove(category);
            category.Name = categoryName;

            this.Collection.Add(category);

            return category;
        }

        public Category Delete(long categoryId)
        {
            var category = this.Collection.Where(c => c.Id == categoryId).FirstOrDefault();
            if(category == null)
            {
                throw new NullReferenceException();
            }

            var itemsWithCategory = this.db.GetAll<Item>().Where(i => i.Categories.Contains(category)).ToList();
            
            foreach(var items in itemsWithCategory)
            {
                items.Categories.Remove(category);
            }

            this.Collection.Remove(category);

            return category;
        }
    }
}
