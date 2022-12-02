using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ThingsLoan.WebAPI.Configuration;
using ThingsLoan.WebAPI.DTO;

namespace ThingsLoan.WebAPI.Handlers
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtOptions _jwtOptions;
        public JwtHandler (IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions?.Value ?? throw new ArgumentNullException (nameof (jwtOptions));
        }
        public string GenerateJWT(UserLoginDto user, IEnumerable<string> roles)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = GetClaims(user, roles);
            var tokenOptions = GenerateTokenOpts(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return token;
        }
        public SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtOptions.Key);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret,SecurityAlgorithms.HmacSha256);
        }
        public List<Claim> GetClaims(UserLoginDto user, IEnumerable<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
        public JwtSecurityToken GenerateTokenOpts(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
                claims: claims,
                issuer:_jwtOptions.Issuer,
                audience:_jwtOptions.Audience,
                signingCredentials: signingCredentials);
            return tokenOptions;
        }
    }
}
