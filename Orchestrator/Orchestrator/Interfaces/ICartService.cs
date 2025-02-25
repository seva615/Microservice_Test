using Orchestrator.API.Models;
using Refit;

namespace Orchestrator.API.Interfaces
{
    public interface ICartService
    {
       public Task<IEnumerable<CartGetModel>> GetAllCarts();

       public Task<CartGetModel> GetCart(Guid cartId);

       public Task CreateCart(Guid userId);

       public Task AddToCart(Guid userId, Guid productId);

       //public Task EditCart(CartGetModel cartGetModel);

       public Task DeleteCart(Guid cartId);
    }
}
