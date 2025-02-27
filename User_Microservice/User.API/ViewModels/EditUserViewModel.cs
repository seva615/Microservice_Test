namespace User.API.ViewModels
{
    public class EditUserViewModel
    {
        public Guid Id { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? Name { get; set; }
    }
}
