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
        public async Task<IEnumerable<ImageViewModel>> GetAllImages()
        {
            var imageModels = await _imageService.GetAllImages();
            var imageViewModels = _mapper.Map<IEnumerable<ImageViewModel>>(imageModels);
            return imageViewModels;
        }

        [HttpPost("addProductImage")] 
        public async Task AddImage(IFormFile image, Guid id)
        {     
            await _imageService.AddImage(image, id);
        }        

        [HttpDelete("deleteImage")]  
        public async Task DeleteImage(Guid id)
        {
            try
            {
                await _imageService.DeleteImage(id);
            }
            catch (Exception e)
            {
                throw new Exception("Image not found");
            }
        }
    }
}
