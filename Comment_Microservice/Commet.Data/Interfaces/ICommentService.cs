using Commet.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commet.Service.Interfaces
{
    public interface ICommentService
    {
        public Task<IEnumerable<CommentModel>> GetAll();

        public Task<IEnumerable<CommentModel>> GetAllProductComments(Guid productId);

        public Task<IEnumerable<CommentModel>> GetAllUserComments(Guid userId);

        public Task<CommentModel> GetComment(Guid commentId);

        public Task EditComment(string comment, Guid commentId);

        public Task DeleteComment(Guid commentId);

        public Task CreateComment(Guid userId, Guid Product, string comment);
    }
}
