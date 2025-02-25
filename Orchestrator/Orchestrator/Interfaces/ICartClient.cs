using Orchestrator.API.Models;
using Refit;

namespace Orchestrator.API.Interfaces
{
    public interface ICartClient
    {
        [Get("/Cart/getAllCarts")]
        Task<IEnumerable<CartGetModel>> GetAllCarts();

        [Get("/Cart/getCart")]
        Task<CartGetModel> GetCart(Guid id);

        [Post("/Cart/createCart")]
        Task AddCart(Guid id);

        [Get("/Cart/addToCart")]
        Task AddToCart(Guid userId, Guid productId);

        [Patch("/Cart/editCart")]
        Task EditCart(CartGetModel cartGetModel);

        [Delete("/Cart/deleteCart")]
        Task DeleteCart(Guid id);
    }
}
