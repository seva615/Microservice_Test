using AutoMapper;
using Product.Data.Entities;
using Product.Data;
using Product.Data.Interfaces;
using Product.Data.Repositories;
using Product.Service.Interfaces;
using Product.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Product.Service.Options;
using Microsoft.Extensions.Options;

namespace Product.Service.Services
{
    public class ImageService : IImageService
    {

        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;        
        private readonly IProductRepository _productRepository;
        private readonly ImageOptions _imageOptions;

        public ImageService(IImageRepository imageRepository, IMapper mapper, IOptions<ImageOptions> imageOptions,
            IProductRepository productRepository)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
            _imageOptions = imageOptions.Value;
            _productRepository = productRepository;
        }
        public async Task DeleteImage(Guid id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }

            await _imageRepository.Delete(id);
        }

        public async Task AddImage(IFormFile image, Guid productId)
        {
            var product = await _productRepository.GetById(productId);

            if (product == null)
            {
                return;
            }

            ImageEntity imageEntity = ConvertToImageEntity(image);
            imageEntity.ProductId = productId;

            if (imageEntity != null)
            {
                product.ProductImages.Add(imageEntity);
            }
            await _productRepository.Edit(product);
        }

        public ImageEntity ConvertToImageEntity(IFormFile image)
        {
            if (image == null)
            {
                throw new ArgumentNullException();
            }

            if (image.Length <= _imageOptions.MaxImageSize
                && _imageOptions.ImageType.Contains(image.ContentType))
            {
                using var binaryReader = new BinaryReader(image.OpenReadStream());

                var imageData = binaryReader.ReadBytes((int)image.Length);

                ImageEntity imageEntity = new ImageEntity()
                {                   
                    Name = image.FileName,
                    Base64 = imageData
                };

                return imageEntity;
            }            
            return null;
        }

        public async Task<IEnumerable<ImageModel>> GetAllImages()
        {
            var imageEntities = await _imageRepository.GetAll();
            var imageModels = _mapper.Map<IEnumerable<ImageModel>>(imageEntities);
            return imageModels;
        }

    }
}
