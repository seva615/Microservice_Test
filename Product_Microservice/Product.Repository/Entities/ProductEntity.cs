using Product.Data.Interfaces;
using Product.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Data.Entities
{
    public class ProductEntity : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public ProductStatus.Stasuses Status { get; set; }

        public ICollection<ImageEntity> ProductImages { get; set; }

        public ProductEntity() 
        {
            ProductImages = new List<ImageEntity>();
        }


    }
}
