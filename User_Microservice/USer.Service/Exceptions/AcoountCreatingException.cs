namespace User.API.Exceptions
{
    public class AccountCreatingException : Exception
    {
        public AccountCreatingException() { }

        public AccountCreatingException(string message)
            : base(message) { }

        public AccountCreatingException(string message, Exception inner)
            : base(message, inner) { }
    }
}
