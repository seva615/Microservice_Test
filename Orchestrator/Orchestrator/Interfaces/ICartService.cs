using Orchestrator.API.Models;
using Refit;

namespace Orchestrator.API.Interfaces
{
    public interface ICartService
    {
       public Task<IEnumerable<CartGetModel>> GetAllCarts();

       public Task<CartGetModel> GetCart(string jwt);

       public Task CreateCart(Guid userId);

       public Task ClearCart(string jwt);

        public Task AddToCart(Guid productId, string jwt, int amount);

       //public Task EditCart(CartGetModel cartGetModel);

       public Task DeleteCart(Guid cartId);
    }
}
