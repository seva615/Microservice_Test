using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

using Product.Data.Entities;
using Product.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Product.Data.Repositories
{
    public class ImageRepository : GenericRepository<ImageEntity>, IImageRepository
    {
        private readonly DataContext _context;

        public ImageRepository(DataContext context) : base(context)
        {
            _context = context;
            CollectionWithIncludes = context.Images;
        }

      
    }
}
