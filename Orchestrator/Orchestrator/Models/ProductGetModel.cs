namespace Orchestrator.API.Models
{
    public class ProductGetModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public string Status { get; set; }

        public ICollection<ImageGetModel> ProductImages { get; set; }
    }
}
