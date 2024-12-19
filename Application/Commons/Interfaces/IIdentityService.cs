using Application.Commons.Models;
using Application.Models;

namespace Application.Commons.Interfaces
{
    public interface IIdentityService
    {
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress);
        Task<Response<string>> CreateUserAsync(CreateUserRequest request, string origin);
        Task<Response<UserResponse>> GetUserById(string id);
        Task<string?> GetUserNameAsync(string userId);
        Task<bool> IsInRoleAsync(string userId, string role);
        Task<bool> AuthorizeAsync(string userId, string policyName);
        Task<Response<string>> DeleteUserAsync(string userId);
        Task<Response<string>> UpdateUserAsync(CreateUserRequest user, string id);
        Task<PaginatedResponse<UserResponse>> GetUsersWithPaginationAndFiltering(FilterRequest request);
    }
}
