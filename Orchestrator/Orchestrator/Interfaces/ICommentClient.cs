using Microsoft.AspNetCore.Mvc;
using Orchestrator.API.Models;
using Refit;

namespace Orchestrator.API.Interfaces
{
    public interface ICommentClient
    {
        [Get("/Comment/getAllComments")]
        Task<IEnumerable<CommentGetModel>> GetAllComments();

        [Post("/Comment/createComment")]
        Task CreateComment(Guid userId, Guid productId, string comment);

        [Get("/Comment/getAllProductComments")]
        Task<IEnumerable<CommentGetModel>> GetAllProductComments(Guid productId);

        [Get("/Comment/getAllUserComments")]
        Task<IEnumerable<CommentGetModel>> GetAllUserComments(Guid userId);

        [Get("/Comment/getComment")]
        Task<CommentGetModel> GetCommentById(Guid commentId);

        [Patch("/Comment/editComment")]
        Task EditComment(string comment, Guid commentId);

        [Delete("/Comment/deleteComment")]
        Task DeleteComment(Guid commentId);

    }
}
