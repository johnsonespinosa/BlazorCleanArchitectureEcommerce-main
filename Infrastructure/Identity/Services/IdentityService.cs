using Application.Commons.Interfaces;
using Application.Commons.Models;
using AutoMapper;
using Domain.Enums;
using Domain.Settings;
using Infrastructure.Identity.Helpers;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Identity.Services
{
    public class IdentityService(
        UserManager<User> userManager,
        IUserClaimsPrincipalFactory<User> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService,
        IOptions<JwtSetting> jwtSettings,
        SignInManager<User> signInManager,
        IMapper mapper)
        : IIdentityService
    {
        private readonly JwtSetting _jwtSettings = jwtSettings.Value;

        public async Task<string?> GetUserNameAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            return user?.UserName;
        }

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = await userManager.FindByIdAsync(userId);
            return user != null && await userManager.IsInRoleAsync(user, role);
        }

        public async Task<bool> AuthorizeAsync(string userId, string policyName)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null) return false;

            var principal = await userClaimsPrincipalFactory.CreateAsync(user);
            var result = await authorizationService.AuthorizeAsync(principal, policyName);

            return result.Succeeded;
        }

        public async Task<Response<string>> DeleteUserAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            return user != null ? await DeleteUserAsync(user) : new Response<string>(userId);
        }

        public async Task<Response<string>> DeleteUserAsync(User user)
        {
            var result = await userManager.DeleteAsync(user);

            if (!result.Succeeded)
                return new Response<string>(
                    message:
                    $"Failed to delete user: {string.Join(", ", result.Errors.Select(error => error.Description))}");

            return new Response<string>(data: user.Id, message: ResponseMessages.EntityDeleted);
        }


        public async Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
        {
            var user = await userManager.FindByEmailAsync(request.Email!);
            if (user is null)
                return new Response<AuthenticationResponse>(
                    message: $"There is no account registered with the email: {request.Email!}.");

            var signInResult = await signInManager.PasswordSignInAsync(user.UserName!, request.Password!, isPersistent: false, lockoutOnFailure: false);
            if (!signInResult.Succeeded)
                return new Response<AuthenticationResponse>(
                    message: $"The credentials are not valid for the user: {user.Email}");

            var securityToken = await GenerateJwtSecurityToken(user);
            var roles = await userManager.GetRolesAsync(user);
            var refreshSecurityToken = GenerateRefreshJwtSecurityToken(ipAddress);

            var authenticationResponse = new AuthenticationResponse()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                JwtToken = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Roles = roles.ToList(),
                IsVerified = user.EmailConfirmed,
                RefreshJwtSecurityToken = refreshSecurityToken.JwtSecurityToken
            };

            return new Response<AuthenticationResponse>(data: authenticationResponse, message: ResponseMessages.AuthenticationSuccessful);
        }

        private RefreshSecurityToken GenerateRefreshJwtSecurityToken(string ipAddress)
        {
            return new RefreshSecurityToken
            {
                JwtSecurityToken = Convert.ToBase64String(inArray: RandomNumberGenerator.GetBytes(count: 64)),
                Expire = DateTime.Now.AddDays(7),
                Created = DateTime.Now,
                CreatedByIp = ipAddress,
            };
        }

        private async Task<JwtSecurityToken> GenerateJwtSecurityToken(User user)
        {
            var userClaims = await userManager.GetClaimsAsync(user);
            var roles = await userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach(var role in roles)
                roleClaims.Add(item: new Claim(type: "roles", value: role));

            var ipAddress = IpHelper.GetIpAddress();
            var claims = new[]
            {
                new Claim(type: JwtRegisteredClaimNames.Sub, value: user.UserName!),
                new Claim(type: JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString()),
                new Claim(type: JwtRegisteredClaimNames.Email, value: user.Email!),
                new Claim(type: "uid", value: user.Id),
                new Claim(type: "ip", value: ipAddress),
            }.Union(userClaims).Union(roleClaims);
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key!));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, algorithm: SecurityAlgorithms.HmacSha256);
            var securityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);

            return securityToken;
        }

        public async Task<Response<string>> CreateUserAsync(CreateUserRequest request, string origin)
        {
            // Check if the user already exists
            var existingUserByUserName = await userManager.FindByNameAsync(request.UserName!);
            if (existingUserByUserName != null)
                return new Response<string>(message: ResponseMessages.EntityAlreadyExists);

            var existingUserByEmail = await userManager.FindByEmailAsync(request.Email!);
            if (existingUserByEmail != null)
                return new Response<string>(message: ResponseMessages.EntityAlreadyExists);

            var user = mapper.Map<User>(request);
            user.PhoneNumberConfirmed = true;
            user.EmailConfirmed = true;

            // Try to create the user
            var result = await userManager.CreateAsync(user, request.Password!);

            if (result.Succeeded)
            {
                // Assign roles to the user
                await userManager.AddToRoleAsync(user, role: Roles.Customer.ToString());
                var response = new Response<string>(user.Id);
                return response;
            }
            return new Response<string>(
                message:
                $"Failed to create admin user: {string.Join(", ", result.Errors.Select(error => error.Description))}");
        }

        public async Task<Response<UserResponse>> GetUserById(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user is null)
                return new Response<UserResponse>(message: ResponseMessages.EntityNotFound);
            
            var data = mapper.Map<UserResponse>(user);
            
            var response = new Response<UserResponse>(data: data, message: ResponseMessages.RecordsRetrievedSuccessfully);
            return response;
        }

        public async Task<Response<string>> UpdateUserAsync(CreateUserRequest user, string id)
        {
            var entity = await userManager.FindByIdAsync(id);

            mapper.Map(user, entity);

            var result = await userManager.UpdateAsync(entity!);

            if (!result.Succeeded)
                return new Response<string>(
                    message: $"Failed to delete user: {string.Join(", ", result.Errors.Select(error => error.Description))}");

            return new Response<string>(data: entity!.Id, message: ResponseMessages.EntityUpdated);
        }

        public Task<PaginatedResponse<UserResponse>> GetUsersWithPaginationAndFiltering(FilterRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
