using Product.Data.Entities;
using Product.Data.Migrations;
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

        public ICollection<CartRecordModel> CartRecords { get; set; }

        public CartModel()
        {
            CartRecords = new List<CartRecordModel>();
        }
    }
}
