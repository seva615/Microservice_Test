using Microsoft.AspNetCore.Http;
using Product.Data.Entities;
using Product.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Service.Interfaces
{
    public interface IImageService
    {
        public Task DeleteImage(Guid id);

        public Task AddImage(IFormFile image, Guid productId);

        public ImageEntity ConvertToImageEntity(IFormFile image);

        public  Task<IEnumerable<ImageModel>> GetAllImages();
        
    }
}
