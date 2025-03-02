namespace Comment.Api
{
    public class CommentViewModel
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid ProductId { get; set; }

        public string Comment { get; set; }
    }
}
