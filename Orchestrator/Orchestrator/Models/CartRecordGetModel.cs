namespace Orchestrator.API.Models
{
    public class CartRecordGetModel
    {
        public Guid Id { get; set; }

        public Guid CartId { get; set; }

        public int Price { get; set; }

        public int ProductAmount { get; set; }

        public ProductGetModel Product { get; set; }
    }
}
