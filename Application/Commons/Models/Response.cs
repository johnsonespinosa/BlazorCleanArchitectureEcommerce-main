namespace Application.Models
{
    public class Response<T>
    {
        public Response()
        {
            Succeeded = true;
            Errors = null;
        }
        public Response(string[]? errors)
        {
            Errors = errors;
            Succeeded = false;
        }

        public Response(T? data)
        {
            Data = data;
            Succeeded = true;
        }

        public bool Succeeded { get; init; }
        public string[]? Errors { get; init; }
        public T? Data { get; set; }
    }
}
