using System.Collections.Generic;

namespace AnswerKing.Core.Entities
{
    public class Item : EntityBase
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public IList<Category> Categories { get; set; }
    }
}
