using Comment.Data.Entity;
using Comment.Data.Interface;
using Microsoft.EntityFrameworkCore;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Npgsql;

namespace Comment.Data.Repositories
{
    public class CommentRepository : GenericRepository<CommentEntity>, ICommentRepository
    {
        private readonly DataContext _context;
        private readonly string _connectionString;
        public CommentRepository(DataContext context, string connectionString) : base(context)
        {
            _context = context;
            CollectionWithIncludes = context.Comments;
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<CommentEntity>> GetAllProductComments(Guid productId)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString)) 
            {
                return await db.QueryAsync<CommentEntity>("SELECT * FROM \"Comments\" WHERE \"ProductId\" = @ProductId", new { ProductId = productId });
            }
        }

        public async Task<IEnumerable<CommentEntity>> GetAllUserComments(Guid userId)
        {
            return await _context.Comments
            .Where(p => p.UserId == userId)
            .ToListAsync();
        }
    }
}
