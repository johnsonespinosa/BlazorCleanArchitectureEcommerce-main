﻿using Application.Commons.Models;
using Application.Interfaces;
using Application.Models;
using Domain.Enums;
using Domain.Settings;
using Infrastructure.Identity.Helpers;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserClaimsPrincipalFactory<User> _userClaimsPrincipalFactory;
        private readonly IAuthorizationService _authorizationService;
        private readonly JwtSetting _jwtSetting;
        private readonly SignInManager<User> _signInManager;

        public IdentityService(
            UserManager<User> userManager,
            IUserClaimsPrincipalFactory<User> userClaimsPrincipalFactory,
            IAuthorizationService authorizationService,
            JwtSetting jwtSetting,
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _authorizationService = authorizationService;
            _jwtSetting = jwtSetting;
            _signInManager = signInManager;
        }

        public async Task<string?> GetUserNameAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user?.UserName;
        }

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user != null && await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<bool> AuthorizeAsync(string userId, string policyName)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return false;
            }

            var principal = await _userClaimsPrincipalFactory.CreateAsync(user);
            var result = await _authorizationService.AuthorizeAsync(principal, policyName);

            return result.Succeeded;
        }

        public async Task<Response<string>> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user != null ? await DeleteUserAsync(user) : new Response<string>(userId);
        }

        public async Task<Response<string>> DeleteUserAsync(User user)
        {
            var result = await _userManager.DeleteAsync(user);
            return new Response<string>(user.Id);
        }

        public async Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
        {
            var user = await _userManager.FindByEmailAsync(request.Email!);
            if (user is null)
                throw new Exception($"No hay una cuenta registrada con el email: {request.Email}.");

            var signInResult = await _signInManager.PasswordSignInAsync(user.UserName!, user.Email!, false, lockoutOnFailure: false);
            if (!signInResult.Succeeded)
                throw new Exception($"Las credenciales no son válidas.");

            var jwtSecurityToken = await GenerateJwtSecurityToken(user);
            var roles = await _userManager.GetRolesAsync(user);
            var refreshJwtSecurityToken = GenerateRefreshJwtSecurityToken(ipAddress);

            var authenticationResponse = new AuthenticationResponse()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                JwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Roles = roles.ToList(),
                IsVerified = user.EmailConfirmed,
                RefreshJwtSecurityToken = refreshJwtSecurityToken.JwtSecurityToken
            };

            return new Response<AuthenticationResponse>(authenticationResponse);
        }

        private RefreshSecurityToken GenerateRefreshJwtSecurityToken(string ipAddress)
        {
            return new RefreshSecurityToken
            {
                JwtSecurityToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expire = DateTime.Now.AddDays(7),
                Created = DateTime.Now,
                CreatedByIp = ipAddress,
            };
        }

        private async Task<JwtSecurityToken> GenerateJwtSecurityToken(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach(var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }

            var ipAddress = IpHelper.GetIpAddress();
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim("uid", user.Id!),
                new Claim("ip", ipAddress),
            }.Union(userClaims).Union(roleClaims);
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Key!));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSetting.Issuer,
                audience: _jwtSetting.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSetting.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        public async Task<Response<string>> CreateUserAsync(UserRequest request, string origin)
        {
            // Verificar si el usuario ya existe
            var existingUserByUserName = await _userManager.FindByNameAsync(request.UserName!);
            if (existingUserByUserName != null)
                throw new Exception($"Ya existe un usuario con el nombre: {request.UserName}");

            var existingUserByEmail = await _userManager.FindByEmailAsync(request.Email!);
            if (existingUserByEmail != null)
                throw new Exception($"Ya existe un usuario con el email: {request.Email}");

            var user = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                EmailConfirmed = true,
                PhoneNumber = request.PhoneNumber,
                PhoneNumberConfirmed = true,
            };

            // Intentar crear el usuario
            var result = await _userManager.CreateAsync(user, request.Password!);
            if (result.Succeeded)
            {
                // Asignar los roles al usuario
                await _userManager.AddToRoleAsync(user, Roles.Customer.ToString());
                return new Response<string>(user.Id); 
            }
            else
            {
                // Manejo de errores: puedes registrar los errores o lanzar excepciones según sea necesario
                throw new Exception($"No se pudo crear el usuario administrador: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }
    }
}
