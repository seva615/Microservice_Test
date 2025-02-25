using Product.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Data.Entities
{
    public class ImageEntity : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public byte[] Base64 { get; set; }
        
        public Guid ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public ProductEntity? Product { get; set; }
    }
}
