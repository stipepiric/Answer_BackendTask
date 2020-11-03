using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnswerBackendTask.Entities;

namespace AnswerBackendTask.Models
{
    public class ItemsDTO
    {
        public IList<Item> Items { get; set; }
    }
}
