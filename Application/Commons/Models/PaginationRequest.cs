namespace Application.Commons.Models
{
    public record PaginationRequest(int PageSize = 10, int PageNumber = 1);
}
