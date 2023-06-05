using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Mango.Services.Auth.Web.Api.Models;
using Mango.Services.Auth.Web.Api.Service.IService;
using Microsoft.Extensions.Options;

namespace Mango.Services.Auth.Web.Api.Service
{
    /// <summary>
    /// This class implements the functions to create a new token according to a success login.
    /// </summary>
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtOptions _jwtOptions;

        public JwtTokenGenerator(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        /// <summary>
        /// Generate a new access token.
        /// </summary>
        /// <param name="applicationUser">Succes user login.</param>
        /// <returns>Token string.</returns>
        public string GenerateToken(ApplicationUser applicationUser)
        {
            // Generate a handler to create token.
            var tokenHandler = new JwtSecurityTokenHandler();
            // Configure the key (secret) for the token.
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);
            // Generate the claims that the token will have (data inside the token).
            // You can create many as you need.
            var claimsList = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, applicationUser.Email),
                new Claim(JwtRegisteredClaimNames.Sub, applicationUser.Id),
                new Claim(JwtRegisteredClaimNames.Name, applicationUser.Name),
            };
            // Generate a descriptor of our token.
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer,
                Subject = new ClaimsIdentity(claimsList),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            // Return the final token that we need.
            return tokenHandler.WriteToken(token);
        }
    }
}
