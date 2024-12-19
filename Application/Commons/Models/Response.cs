namespace Application.Models
{
    public class Response<T>
    {
        public Response() { }

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

        public bool Succeeded { get; set; }
        public string[]? Errors { get; set; }
        public T? Data { get; set; }
    }
}
