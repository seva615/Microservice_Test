using Microsoft.AspNetCore.Mvc;
using Orchestrator.API.Interfaces;
using Orchestrator.API.Models;
using Orchestrator.API.Services;

namespace Orchestrator.API.Controllers
{
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("getAllComments")]
        public async Task<IActionResult> GetAllComments()
        {
            
            try
            {
                var comments = await _commentService.GetAllComments();
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("createComment")]
        [JwtAuthorizationFilter()]
        public async Task<IActionResult> CreateComment(Guid productId, string comment)
        {
            var httpContext = HttpContext;
            string jwt = httpContext.Request.Headers["Authorization"].FirstOrDefault();
            try
            {
                await _commentService.CreateComment(jwt, productId, comment);
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
            
            try
            {
                var comments = await _commentService.GetAllProductComments(productId);
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("getAllUserComments")]
        [JwtAuthorizationFilter()]
        public async Task<IActionResult> GetAllUSerComments()
        {
            var httpContext = HttpContext;
            string jwt = httpContext.Request.Headers["Authorization"].FirstOrDefault();
            try
            {
               var comments = await _commentService.GetAllUserComments(jwt);
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("getComment")]
        public async Task<IActionResult> GetCommentById(Guid commentId)
        {
            
            try
            {
               var comment = await _commentService.GetCommentById(commentId);
                return Ok(comment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
