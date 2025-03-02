using Comment.Data.Entity;
using Comment.Data.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comment.Data.Repositories
{
    public class CommentRepository : GenericRepository<CommentEntity>, ICommentRepository
    {
        private readonly DataContext _context;
        public CommentRepository(DataContext context) : base(context)
        {
            _context = context;
            CollectionWithIncludes = context.Comments;
        }

        public async Task<IEnumerable<CommentEntity>> GetAllProductComments(Guid productId)
        {
            return await _context.Comments
            .Where(p => p.ProductId == productId)
            .ToListAsync();
        }

        public async Task<IEnumerable<CommentEntity>> GetAllUserComments(Guid userId)
        {
            return await _context.Comments
            .Where(p => p.UserId == userId)
            .ToListAsync();
        }
    }
}
