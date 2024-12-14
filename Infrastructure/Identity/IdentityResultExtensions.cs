using Application.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public static class IdentityResultExtensions
    {
        public static Response<string> ToApplicationResult(this IdentityResult result)
        {
            return result.Succeeded
                ? new Response<string>()
                : new Response<string>(result.Errors.Select(e => e.Description).ToArray());
        }
    }
}
