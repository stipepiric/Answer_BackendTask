using System;
using System.Collections.Generic;
using System.Text;
using AnswerKing.Core.Entities;

namespace AnswerKing.Infrastructure.Repository
{
    public interface ICategoryRepository
    {
        public IList<Category> GetAll();
        public Category Create(string categoryName);
        public Item AddItemToCategory(long itemId, long categoryId);
        public Item RemoveItemFromCategory(long itemId, long categoryId);
        public Category Edit(long categoryId, string categoryName);
        public Category Delete(long categoryId);
    }
}
