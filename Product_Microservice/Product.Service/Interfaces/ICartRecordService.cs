using Product.Data.Entities;
using Product.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Service.Interfaces
{
    public interface ICartRecordService
    {
        public Task DeleteCartRecord(Guid id);

        public Task<CartRecordEntity> CreateCartRecord(Guid productId, Guid cartId, int amount);

        public Task<CartRecordModel> GetCartRecord(Guid id);

        public Task<CartRecordEntity?> GetCartRecordByCartId(Guid cartId);

        public Task EditCartRecord(CartRecordModel cartRecord);

        public Task<IEnumerable<CartRecordModel>> GetAllCartRecords();
        
    }
}
