namespace Orchestrator.API.Models
{
    public class CartGetModel
    {
        public Guid Id { get; set; }

        public Guid? UserId { get; set; }

        public int TotalPrice { get; set; }

        public ICollection<ProductGetModel> Products { get; set; }
    }
}
