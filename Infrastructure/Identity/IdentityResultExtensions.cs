using Application.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public static class IdentityResultExtensions
    {
        public static WritingResponse ToApplicationResult(this IdentityResult result)
        {
            return result.Succeeded
                ? WritingResponse.Success()
                : WritingResponse.Failure(result.Errors.Select(e => e.Description));
        }
    }
}
