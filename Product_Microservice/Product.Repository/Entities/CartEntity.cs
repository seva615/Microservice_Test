using Product.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Data.Entities
{
    public class CartEntity : IEntity
    {
        public Guid Id { get; set; }

        public Guid? UserId { get; set; }

        public int TotalPrice { get; set; }

        public ICollection<CartRecordEntity> CartRecords { get; set; }

        public CartEntity()
        {
            CartRecords = new List<CartRecordEntity>();
        }

    }
}
