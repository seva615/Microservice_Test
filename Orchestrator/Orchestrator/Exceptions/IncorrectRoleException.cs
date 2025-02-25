namespace Orchestrator.API.Exceptions
{
    public class IncorrectRoleException : Exception
    {
        public IncorrectRoleException() { }

        public IncorrectRoleException(string message)
            : base(message) { }

        public IncorrectRoleException(string message, Exception inner)
            : base(message, inner) { }
    }
}
