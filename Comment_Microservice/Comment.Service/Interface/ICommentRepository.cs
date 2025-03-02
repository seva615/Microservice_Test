using Comment.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comment.Data.Interface
{
    public interface ICommentRepository : IGenericRepository<CommentEntity>
    {
        public Task<IEnumerable<CommentEntity>> GetAllProductComments(Guid productId);

        public Task<IEnumerable<CommentEntity>> GetAllUserComments(Guid userId);
    }
}
