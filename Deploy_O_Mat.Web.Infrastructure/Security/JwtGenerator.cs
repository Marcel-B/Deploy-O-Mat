using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using com.b_velop.Deploy_O_Mat.Web.Identity.Models;
using com.b_velop.Utilities.Docker;
using Deploy_O_Mat.Web.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace com.b_velop.Deploy_O_Mat.Web.Infrastructure.Security
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly SecretProvider _secretProvider;
        public JwtGenerator(SecretProvider secretProvider)
        {
            _secretProvider = secretProvider;
        }

        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
            };

            var secret = _secretProvider.GetSecret("identity_signing_key");
            // generate signing credentials
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
