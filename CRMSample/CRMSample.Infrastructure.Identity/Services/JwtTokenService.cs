using CRMSample.Application.Common.Services;
using CRMSample.Application.Identity.Services;
using CRMSample.Domain.Identity.Entities.Account;
using CRMSample.Infrastructure.Common.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CRMSample.Infrastructure.Identity.Services
{
    public class JwtTokenService : ITokenService
    {
        private readonly AuthenticationSettings _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDateTime _dateTime;

        public JwtTokenService(
            AuthenticationSettings configuration, 
            UserManager<ApplicationUser> userManager, 
            IDateTime dateTime)
        {
            _configuration = configuration;
            _userManager = userManager;
            _dateTime = dateTime;
        }

        public async Task<CreateTokenResult> CreateTokenAsync(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.AuthenticationKey);

            // add the claims for rbac
            ClaimsIdentity claims = GetClaims(user);

            // add the specific roles held by the ApplicationUser
            await AddRolesAsync(user, claims);

            // create the token itself for the ApplicationUser
            SecurityToken token = CreateToken(tokenHandler, key, claims);

            string tokenValue = tokenHandler.WriteToken(token);
            return new CreateTokenResult(tokenValue)
            {
                ValidFrom = token.ValidFrom,
                ValidTo = token.ValidTo
            };
        }

        private static ClaimsIdentity GetClaims(ApplicationUser user)
        {
            return new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                });
        }

        private async Task AddRolesAsync(ApplicationUser user, ClaimsIdentity claims)
        {
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.AddClaim(new Claim(ClaimTypes.Role, role));
            }
        }

        private SecurityToken CreateToken(JwtSecurityTokenHandler tokenHandler, byte[] key, ClaimsIdentity claims)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration.Issuer,
                Audience = _configuration.Audience,
                Subject = claims,
                NotBefore = _dateTime.Now,
                Expires = _dateTime.Now.AddMinutes(_configuration.ExpiryTimeInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return token;
        }

        private static IEnumerable<Claim> GetDefaultClaims(ApplicationUser user, string issuer, string audience)
        {
            return new[]
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name,  user.UserName),
                new Claim(JwtRegisteredClaimNames.Sub,  user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Iss, issuer),
                new Claim(JwtRegisteredClaimNames.Aud, audience),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("username", user.UserName)
            };
        }
    }
}
