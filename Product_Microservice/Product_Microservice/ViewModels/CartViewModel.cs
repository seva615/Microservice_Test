using Product.Service.Models;

namespace Product.Api.ViewModels
{
    public class CartViewModel
    {
        public Guid Id { get; set; }

        public Guid? UserId { get; set; }

        public int TotalPrice { get; set; }

        public ICollection<CartRecordViewModel> CartRecords { get; set; }
    }
}
