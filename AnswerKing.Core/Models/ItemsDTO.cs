using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnswerKing.Core.Entities;

namespace AnswerKing.Core.Models
{
    public class ItemsDTO
    {
        public IList<Item> Items { get; set; }
    }
}
