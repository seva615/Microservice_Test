using Microsoft.AspNetCore.Mvc;
using Orchestrator.API.Models;
using Refit;

namespace Orchestrator.API.Interfaces
{
    public interface IImageClient
    {
        [Get("/Image/getAllImages")]
        public Task<IEnumerable<ImageGetModel>> GetAllImagess();

        [Multipart]
        [Post("/Image/addProductImage")]
        public Task AddImage(StreamPart image,[Query]Guid id);

        [Delete("/Image/deleteImage")]
        public Task DeleteImage(Guid id);
    }
}
