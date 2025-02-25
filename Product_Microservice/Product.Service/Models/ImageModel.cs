using Product.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Service.Models
{
    public class ImageModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public byte[] Base64 { get; set; }

        public ProductEntity? Product { get; set; }
        
        public Guid ProductId { get; set; }
    }
}
