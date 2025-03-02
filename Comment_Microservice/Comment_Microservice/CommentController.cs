using AutoMapper;
using Commet.Service.Interfaces;
using Commet.Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace Comment.Api
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentController(ICommentService commentService, IMapper mapper)
        {
            _mapper = mapper;
            _commentService = commentService;
        }

        [HttpGet("getAllComments")]
        public async Task<IActionResult> GetAllComments()
        {
            IEnumerable<CommentModel> commentModels;
            try
            {
                commentModels = await _commentService.GetAll();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }             
            var commentViewModels = _mapper.Map<IEnumerable<CommentViewModel>>(commentModels);
            return Ok(commentViewModels);
        }

        [HttpPost("createComment")]
        public async Task<IActionResult> CreateComment(Guid userId, Guid productId, string comment)
        {
            try
            {
                await _commentService.CreateComment(userId, productId, comment);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("getAllProductComments")]
        public async Task<IActionResult> GetAllProductComments(Guid productId)
        {
            IEnumerable<CommentModel> commentModels;
            try
            {
                commentModels = await _commentService.GetAllProductComments(productId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            var commentViewModels = _mapper.Map<IEnumerable<CommentViewModel>>(commentModels);
            return Ok(commentViewModels);
        }

        [HttpGet("getAllUserComments")]
        public async Task<IActionResult> GetAllUSerComments(Guid userId)
        {
            IEnumerable<CommentModel> commentModels;
            try
            {
                commentModels = await _commentService.GetAllUserComments(userId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            var commentViewModels = _mapper.Map<IEnumerable<CommentViewModel>>(commentModels);
            return Ok(commentViewModels);
        }

        [HttpGet("getComment")]
        public async Task<IActionResult> GetCommentById(Guid commentId)
        {
            CommentModel commentModel;
            try
            {
                commentModel = await _commentService.GetComment(commentId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            var commentViewModel = _mapper.Map<CommentModel, CommentViewModel>(commentModel);
            return Ok(commentViewModel);
        }

        [HttpPatch("editComment")]
        public async Task<IActionResult> EditComment(string comment, Guid commentId)
        {
            try
            {
                await _commentService.EditComment(comment, commentId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [HttpDelete("deleteComment")]
        public async Task<IActionResult> DeleteComment(Guid commentId)
        {           
            try
            {
                await _commentService.DeleteComment(commentId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
