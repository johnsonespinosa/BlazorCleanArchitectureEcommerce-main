namespace Application.Commons.Exceptions
{
    public class NotFoundException: Exception
    {
        public NotFoundException() : base("No se encontraron registros") { }
    }
}
