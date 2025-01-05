namespace Application.Commons.Models
{
    public class Response<T>
    {
        public Response() { }

        public Response(string message)
        {
            Message = message;
            Succeeded = false;
        }

        public Response(string[]? errors, string message)
        {
            Message = message;
            Errors = errors;
            Succeeded = false;
        }

        public Response(T? data, string message)
        {
            Data = data;
            Message = message;
            Succeeded = true;
            Errors = [];
        }

        public bool Succeeded { get; init; }
        public string? Message { get; init; }
        public string[]? Errors { get; init; }
        public T? Data { get; init; }
    }
}
