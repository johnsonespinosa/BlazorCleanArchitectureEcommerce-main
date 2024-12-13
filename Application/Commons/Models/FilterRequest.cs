namespace Application.Models
{
    public record FilterRequest(string Text = null!) : PaginationRequest;
}
