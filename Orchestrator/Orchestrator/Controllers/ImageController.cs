using Microsoft.AspNetCore.Mvc;
using Orchestrator.API.Interfaces;
using Orchestrator.API.Models;

namespace Orchestrator.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet("GeatAllImages")]
        public async Task<IEnumerable<ImageGetModel>> GetAllImages()
        {
            var images = await _imageService.GetAllImagess();
            return images;
        }

        [HttpPost("AddImage")]
        public async Task AddImage(IFormFile image, Guid productId)
        {
            await _imageService.AddProductImage(image, productId);
        }

        [HttpDelete("DeleteImage")]
        public async Task DeleteImage(Guid id)
        {
            await _imageService.DeleteImage(id);
        }

    }
}
