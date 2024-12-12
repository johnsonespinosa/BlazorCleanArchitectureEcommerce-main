namespace Application.Models
{
    public class WritingResponse
    {
        internal WritingResponse(bool succeeded, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public bool Succeeded { get; init; }

        public string[] Errors { get; init; }

        public static WritingResponse Success()
        {
            return new WritingResponse(true, Array.Empty<string>());
        }

        public static WritingResponse Failure(IEnumerable<string> errors)
        {
            return new WritingResponse(false, errors);
        }
    }
}
