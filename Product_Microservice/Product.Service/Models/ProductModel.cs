using Product.Data;
using Product.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Service.Models
{
    public class ProductModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public ProductStatus.Stasuses Status { get; set; }

        public ICollection<ImageModel> ProductImages { get; set; }

        public ProductModel()
        {
            ProductImages = new List<ImageModel>();
        }
    }
}
