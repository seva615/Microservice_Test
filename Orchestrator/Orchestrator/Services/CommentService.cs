using ICSharpCode.Decompiler.Solution;
using Orchestrator.API.Interfaces;
using Orchestrator.API.Models;

namespace Orchestrator.API.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUserClient _userClient;
        private readonly IProductClient _productClient;
        private readonly ICommentClient _commentClient;

        public CommentService(ICommentClient commentClient, IUserClient userClient, IProductClient productClient)
        {
            _commentClient = commentClient;
            _userClient = userClient;
            _productClient = productClient;
        }

        public async Task CreateComment(string jwt, Guid productId, string comment)
        {
            var user = await _userClient.GetUserByJwt(jwt);
            var product = await _productClient.GetProduct(productId);
            var comments = await _commentClient.GetAllProductComments(productId);

            if(comments.Any(e => e.UserId == user.Id)){
                throw new Exception("Comment on this product by user is already exist");
            }

            if (user == null)
            {
                throw new Exception("User not found");
            }
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            
            await _commentClient.CreateComment(user.Id, productId, comment);

        }

        public async Task DeleteComment(Guid commentId)
        {
            await _commentClient.DeleteComment(commentId);
        }

        public async Task EditComment(string comment, Guid commentId)
        {
            await _commentClient.EditComment(comment, commentId);
        }

        public async Task<IEnumerable<CommentGetModel>> GetAllComments()
        {
            var comments = await _commentClient.GetAllComments();
            return comments;
        }

        public async Task<IEnumerable<CommentGetModel>> GetAllProductComments(Guid productId)
        {
            var comments = await _commentClient.GetAllProductComments(productId);
            return comments;
        }

        public async Task<IEnumerable<CommentGetModel>> GetAllUserComments(string jwt)
        {
            var user = await _userClient.GetUserByJwt(jwt);
            var comments = await _commentClient.GetAllUserComments(user.Id);
            return comments;
        }

        public async Task<CommentGetModel> GetCommentById(Guid commentId)
        {
            var comment = await _commentClient.GetCommentById(commentId);
            return comment;
        }
    }
}
