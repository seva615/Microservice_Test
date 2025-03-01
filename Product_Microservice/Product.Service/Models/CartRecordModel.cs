using Product.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Service.Models
{
    public class CartRecordModel
    {
        public Guid Id { get; set; }

        public Guid CartId { get; set; }

        public int Price { get; set; }

        public int ProductAmount { get; set; }

        public ProductModel Product { get; set; }
        public CartModel? Cart { get; set; }
    }
}
