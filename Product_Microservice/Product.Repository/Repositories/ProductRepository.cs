using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Product.Data.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product.Data.Interfaces;

namespace Product.Data.Repositories
{
    public class ProductRepository : GenericRepository<ProductEntity>, IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context) : base(context)
        {
            _context = context;
            CollectionWithIncludes = context.Products
                .Include(c => c.ProductImages);
        }
              
        
    }
}
