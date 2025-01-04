namespace Application.Commons.Models
{
    public record FilterRequest(string Text = null!) : PaginationRequest;
}
