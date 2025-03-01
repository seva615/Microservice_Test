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
        public async Task<IActionResult> GetAllImages()
        {
            try
            {
                var images = await _imageService.GetAllImagess();
                return Ok(images);
            }
            catch (Exception ex)
            {
               return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddImage")]
        public async Task<IActionResult> AddImage(IFormFile image, Guid productId)
        {
            try
            {
                await _imageService.AddProductImage(image, productId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteImage")]
        public async Task<IActionResult> DeleteImage(Guid id)
        {
            try
            {
                await _imageService.DeleteImage(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
