using Product.Data;
using Product.Data.Entities;

namespace Product.Api.ViewModels
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public string Status { get; set; }

        public ICollection<ImageViewModel> ProductImages { get; set; }
    }
}
