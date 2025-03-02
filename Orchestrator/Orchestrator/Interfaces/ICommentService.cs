using Orchestrator.API.Models;
using Refit;

namespace Orchestrator.API.Interfaces
{
    public interface ICommentService
    {
      public Task<IEnumerable<CommentGetModel>> GetAllComments();

      public Task CreateComment(string jwt, Guid productId, string comment);
      
      public Task<IEnumerable<CommentGetModel>> GetAllProductComments(Guid productId);
      
      public Task<IEnumerable<CommentGetModel>> GetAllUserComments(string jwt);
      
      public Task<CommentGetModel> GetCommentById(Guid commentId);
      
      public Task EditComment(string comment, Guid commentId);
      
      public Task DeleteComment(Guid commentId);
    }
}
