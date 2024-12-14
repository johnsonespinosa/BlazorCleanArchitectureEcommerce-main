namespace Application.Commons.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string email)
            : base($"No hay una cuenta registrada con el email: {email}.") { }
    }

}
