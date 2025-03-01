using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product.Api.ViewModels;
using Product.Service.Interfaces;
using Product.Service.Models;
using Product.Service.Services;

namespace Product.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;

        public ImageController(IImageService imageService, IMapper mapper)
        {
            _mapper = mapper;
            _imageService = imageService;
        }

        [HttpGet("getAllImages")]        
        public async Task<IActionResult> GetAllImages()
        {
            IEnumerable<ImageModel> imageModels;
            try
            {
                imageModels = await _imageService.GetAllImages();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            var imageViewModels = _mapper.Map<IEnumerable<ImageViewModel>>(imageModels);
            return Ok(imageViewModels);
        }

        [HttpPost("addProductImage")] 
        public async Task<IActionResult> AddImage(IFormFile image, Guid id)
        {
            try
            {
                await _imageService.AddImage(image, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            
        }        

        [HttpDelete("deleteImage")]  
        public async Task<IActionResult> DeleteImage(Guid id)
        {
            try
            {
                await _imageService.DeleteImage(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
