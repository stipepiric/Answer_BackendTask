using System;
using System.Collections.Generic;
using System.Text;
using AnswerKing.Core.Entities;

namespace AnswerKing.Infrastructure.Repository
{
    public interface IItemRepository
    {
        IList<Item> GetAll();
        Item Get(long id);
        Item Create(string itemName);
        Item Update(Item item);

        Item Delete(long id);
    }
}
