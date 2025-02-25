using Product.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Service.Models
{
    public class CartModel
    {
        public Guid Id { get; set; }

        public Guid? UserId { get; set; }

        public int TotalPrice { get; set; }

        public ICollection<ProductModel> Products { get; set; }

        public CartModel()
        {
            Products = new List<ProductModel>();
        }
    }
}
