using AutoMapper;
using Comment.Data.Entity;
using Comment.Data.Interface;
using Commet.Service.Interfaces;
using Commet.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commet.Service.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task DeleteComment(Guid commentId)
        {
           await _commentRepository.Delete(commentId);
        }

        public async Task EditComment(string comment, Guid commentId)
        {
            var commentEntity = await _commentRepository.GetById(commentId);
            if (commentEntity == null)
            {
                throw new Exception("Comment not found");
            }
            commentEntity.Comment = comment;
            await _commentRepository.Edit(commentEntity);
        }

        public async Task<IEnumerable<CommentModel>> GetAll()
        {
            var commentEntities = await _commentRepository.GetAll();
            var commentModels = _mapper.Map<IEnumerable<CommentModel>>(commentEntities);
            return commentModels;
        }

        public async Task<IEnumerable<CommentModel>> GetAllProductComments(Guid productId)
        {
            var commentEntities = await _commentRepository.GetAllProductComments(productId);
            var commentModels = _mapper.Map<IEnumerable<CommentModel>>(commentEntities);
            return commentModels;
        }

        public async Task<IEnumerable<CommentModel>> GetAllUserComments(Guid userId)
        {
            var commentEntities = await _commentRepository.GetAllUserComments(userId);
            var commentModels = _mapper.Map<IEnumerable<CommentModel>>(commentEntities);
            return commentModels;
        }

        public async Task<CommentModel> GetComment(Guid commentId)
        {
            var commentEntity = await _commentRepository.GetById(commentId);
            if (commentEntity == null)
            {
                throw new Exception("Comment not found");
            }
            var commentModel = _mapper.Map<CommentEntity, CommentModel>(commentEntity);
            return commentModel;
        }

        public async Task CreateComment(Guid userId, Guid productId, string comment)
        {
            if(comment == null)
            {
                throw new Exception("Comment is empty");
            }
            CommentEntity newComment = new CommentEntity
            {
                UserId = userId,
                Comment = comment,
                ProductId = productId
            };
           await _commentRepository.Add(newComment); 
        }
    }
}
