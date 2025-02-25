using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data.Entities
{
    public class CategoryEntity : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<ProductEntity> Products { get; set; }

        public CategoryEntity() 
        { 
        Products = new List<ProductEntity>();
        }
    }
}
