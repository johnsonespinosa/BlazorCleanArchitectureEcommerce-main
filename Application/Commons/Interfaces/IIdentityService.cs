using Application.Models;

namespace Application.Interfaces
{
    public interface IIdentityService
    {
        Task<string?> GetUserNameAsync(string userId);

        Task<bool> IsInRoleAsync(string userId, string role);

        Task<bool> AuthorizeAsync(string userId, string policyName);

        Task<(WritingResponse Response, string UserId)> CreateUserAsync(string userName, string password);

        Task<WritingResponse> DeleteUserAsync(string userId);
    }
}
