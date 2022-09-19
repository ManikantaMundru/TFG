using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TFG.Assessment.Api.Interfaces;
using TFG.Assessment.Api.Settings;
using TFG.Assessment.Domain.Entities;
using TFG.Assessment.Domain.Exceptions;
using TFG.Assessment.Domain.Interfaces;
using TFG.Assessment.Domain.Requests;
using TFG.Assessment.Domain.Response;

namespace TFG.Assessment.Api.Services
{
    public class TokenService : ITokenService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppSettings _appSettings;
        public TokenService(IUnitOfWork unitOfWork, IOptions<AppSettings> appSettings)
        {
            _unitOfWork = unitOfWork;
            _appSettings = appSettings.Value;
        }
        public async Task<TokenResponse> Authenticate(AuthenticationRequest authRequest)
        {
            var users = await _unitOfWork.Users.Find(x => x.UserName == authRequest.Username && x.Password == authRequest.Password);
            if (users == null)
            {
                throw new AppException("Username or password is incorrect");
            }
            var user = users.FirstOrDefault();
            var token = GenerateJwtToken(user);

            return new TokenResponse
            {
                JwtToken = token
            };
        }

        private string GenerateJwtToken(User user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.ClientSecret));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>() {

                new Claim(ClaimTypes.Name , user.Id.ToString()),
                new Claim(ClaimTypes.Role , user.Role)
            };

            var tokeOptions = new JwtSecurityToken(
                issuer: _appSettings.ValidIssuer,
                audience: _appSettings.ValidAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(6),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            return tokenString;
        }
    }
}
