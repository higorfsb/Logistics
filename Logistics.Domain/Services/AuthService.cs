using Logistics.Domain.Builders.User;
using Logistics.Domain.Dto.User;
using Logistics.Domain.Entities;
using Logistics.Domain.Helpers;
using Logistics.Domain.Interfaces.Builders;
using Logistics.Domain.Interfaces.Repositories;
using Logistics.Domain.Interfaces.Services;
using Logistics.Domain.Settings.ErrorHandler.ErrorStatusCode;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Domain.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILoginResponseBuilder _loginResponseBuilder;
        private readonly JwtSettings _jwtSettings;

        public AuthService(IOptions<JwtSettings> jwtSettings,
            IUserRepository userRepository,
            ILoginResponseBuilder loginResponseBuilder)
        {
            _jwtSettings = jwtSettings.Value;
            _userRepository = userRepository;
            _loginResponseBuilder = loginResponseBuilder;
        }

        public async Task<LoginResponse> LoginAsync(string userName, string password)
        {
            User user = await _userRepository.ValidateLoginAsync(userName, password);

            if (user == null)
                throw new NotFoundException("User or password are invalids");

            ClaimsIdentity userClaims = GetUserClaims(user);

            string token = EncodeToken(userClaims);

            LoginResponse loginResponse = GetResponseToken(token, userClaims.Claims);

            return loginResponse;
        }

        private static ClaimsIdentity GetUserClaims(User user)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()),
                new(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64),
                new("UserFullName", user.Name),
                new("UserCode", user.Id.ToString())
            };

            var identityClaims = new ClaimsIdentity();

            identityClaims.AddClaims(claims);

            return identityClaims;
        }

        private string EncodeToken(ClaimsIdentity identityClaims)
        {
            var tokenHandler = new JwtSecurityTokenHandler()
            {
                SetDefaultTimesOnTokenCreation = false
            };

            byte[] key = Encoding.ASCII.GetBytes(_jwtSettings.Key);

            SecurityToken token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddDays(9999),
                NotBefore = DateTime.UtcNow.AddDays(-10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }

        private LoginResponse GetResponseToken(string encodedToken, IEnumerable<Claim> claims)
        {
            return new LoginResponse
            {
                AccessToken = _loginResponseBuilder.BuildAccessToken(encodedToken),
                UserToken = _loginResponseBuilder.BuildUserToken(claims)
            };
        }

        private static long ToUnixEpochDate(DateTime date)
        => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
