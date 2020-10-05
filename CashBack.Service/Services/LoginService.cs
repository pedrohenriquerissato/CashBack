using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using CashBack.Domain.Models;
using CashBack.Domain.Repositories;
using CashBack.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace CashBack.Service.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUnitOfWork _unitOfWork;

        private SigningConfiguration _signingConfiguration;
        private TokenConfiguration _tokenConfiguration;
        private IConfiguration _configuration { get; }

        public LoginService(IUnitOfWork unitOfWork, SigningConfiguration signingConfiguration, TokenConfiguration tokenConfiguration, IConfiguration configuration)
        {
            this._unitOfWork = unitOfWork;
            this._signingConfiguration = signingConfiguration;
            this._tokenConfiguration = tokenConfiguration;
            this._configuration = configuration;
        }

        public async Task<object> Login(string email, string password)
        {
            if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(password))
            {
                var retailer = await _unitOfWork.Logins.Login(email, password);

                if (retailer == null)
                {
                    goto NotAuthorized;
                }

                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(email),
                    new[]
                    {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, email),
                    }

                );

                DateTime createDateTime = DateTime.Now;
                DateTime expirationDate = createDateTime + TimeSpan.FromSeconds(_tokenConfiguration.Seconds);

                var handler = new JwtSecurityTokenHandler();
                var token = CreateToken(identity, createDateTime, expirationDate, handler);

                return SuccessObject(createDateTime, expirationDate, token, email);

            }

            NotAuthorized:
            return new
            {
                authenticated = false,
                message = "Falha ao realizar login"
            };
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate,
            JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfiguration.Issuer,
                Audience = _tokenConfiguration.Audience,
                SigningCredentials = _signingConfiguration.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate,
            });

            var token = handler.WriteToken(securityToken);
            return token;
        }

        private object SuccessObject(DateTime createDateTime, DateTime expirationDateTime, string token, string email)
        {
            return new
            {
                authenticated = true,
                created = createDateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                userName = email,
                message = "Login realizado com sucesso"
            };
        }
    }
}
