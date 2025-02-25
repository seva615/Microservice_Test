using Orchestrator.API.Interfaces;
using Orchestrator.API.Models;
using Refit;

namespace Orchestrator.API.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageClient _imageClient;

        public ImageService(IImageClient imageClient)
        {
            _imageClient = imageClient;
        }

        public async Task<IEnumerable<ImageGetModel>> GetAllImagess()
        {
            var images = await _imageClient.GetAllImagess();
            return images;
        }

        public async Task AddProductImage(IFormFile image, Guid id)
        {
            StreamPart imageStream = ToStreamPart(image);
            await _imageClient.AddImage(imageStream, id);
        }

        public StreamPart ToStreamPart(IFormFile file)
        {
            if (file == null)
            {
                return null;
            }

            Stream stream = file.OpenReadStream();
            string fileName = file.FileName;
            string contentType = file.ContentType;

            return new StreamPart(stream, fileName, contentType);
        }

        public async Task DeleteImage(Guid id)
        {
            await _imageClient.DeleteImage(id);
        }

    }
}
