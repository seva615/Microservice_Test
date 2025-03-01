using Product.Data.Entities;

namespace Product.Api.ViewModels
{
    public class CartRecordViewModel
    {
        public Guid Id { get; set; }

        public Guid CartId { get; set; }

        public int Price { get; set; }

        public int ProductAmount { get; set; }

        public ProductViewModel Product { get; set; }
    }
}
