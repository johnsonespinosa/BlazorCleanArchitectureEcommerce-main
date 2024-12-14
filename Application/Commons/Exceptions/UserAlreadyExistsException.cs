namespace Application.Commons.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException(string value)
            : base($"Ya existe un usuario con el nombre o el email: {value}") { }
    }
}
