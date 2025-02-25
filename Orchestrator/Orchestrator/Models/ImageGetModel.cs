namespace Orchestrator.API.Models
{
    public class ImageGetModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public byte[] Base64 { get; set; }

        public Guid ProductId { get; set; }
    }
}
