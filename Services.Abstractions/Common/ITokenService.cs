﻿using Microsoft.IdentityModel.Tokens;
using MyProperty.API.Core.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Services.Abstractions
{
	public interface ITokenService
    {
        SigningCredentials GetSigningCredentials();

        Task<List<Claim>> GetClaims(Account account);

        JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims);

        string GenerateRefreshToken();

        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
