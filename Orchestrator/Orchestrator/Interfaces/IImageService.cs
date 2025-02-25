using Microsoft.AspNetCore.Http;
using Orchestrator.API.Models;
using Refit;

namespace Orchestrator.API.Interfaces
{
    public interface IImageService
    {
        public Task<IEnumerable<ImageGetModel>> GetAllImagess();
               
        public Task AddProductImage(IFormFile image, Guid productId);

        public  StreamPart ToStreamPart(IFormFile file);

        public Task DeleteImage(Guid id);
    }
}
