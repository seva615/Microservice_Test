using Orchestrator.API.Models;
using Refit;

namespace Orchestrator.API.Interfaces
{
    public interface ICartClient
    {
        [Get("/Cart/getAllCarts")]
        Task<IEnumerable<CartGetModel>> GetAllCarts();

        [Get("/Cart/getCart")]
        Task<CartGetModel> GetCart(Guid userId);

        [Post("/Cart/createCart")]
        Task AddCart(Guid userId);

        [Get("/Cart/addToCart")]
        Task AddToCart(Guid userId, Guid productId, int amount);

        [Patch("/Cart/clearCart")]
        Task ClearCart(Guid userId);

        [Patch("/Cart/editCart")]
        Task EditCart(CartGetModel cartGetModel);

        [Delete("/Cart/deleteCart")]
        Task DeleteCart(Guid id);
    }
}
