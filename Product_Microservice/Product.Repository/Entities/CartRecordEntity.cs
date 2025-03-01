using Product.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Data.Entities
{
    public class CartRecordEntity : IEntity
    {
        public Guid Id { get; set; }

        public Guid CartId { get; set; }

        public int Price { get; set; }

        public int ProductAmount { get; set; }

        public ProductEntity Product { get; set; }

        [ForeignKey(nameof(CartId))]
        public CartEntity? Cart { get; set; }
    }
}
