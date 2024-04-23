using BAL.Services.Interfaces;
using BAL.ViewModels.Authenticates;
using DAL.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Implements
{
    public class JWTService : IJWTService
    {
        public ObjectToken ExtractToken(string token, IConfiguration config)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(config["AppSettings:SecretKey"]);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            }, out var validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var username = jwtToken.Claims.First(x => x.Type == "unique_name").Value;
            var role = jwtToken.Claims.First(x => x.Type == "role").Value;
            var UserId = jwtToken.Claims.First(x => x.Type == "nameid").Value;
            return new ObjectToken
            {
                Username = username,
                Role = role,
                UserId = UserId,
            };
        }

        public string GenerateJWTToken(string userID, string username, string role, IConfiguration config, DateTime? expir = null)
        {
            var key = Encoding.ASCII.GetBytes(config["AppSettings:SecretKey"]);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userID),
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, role)
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            if (expir != null)
            {
				// Thời gian hết hạn của JWT
				tokenDescriptor.Expires = expir;
            }
			// Thời gian hết hạn của JWT
			else tokenDescriptor.Expires = DateTime.UtcNow.AddHours(1);

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }

		public string GenerateJWTToken(string username, string role, IConfiguration config, DateTime? expir = null)
		{
			var key = Encoding.ASCII.GetBytes(config["AppSettings:SecretKey"]);
			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.Name, username),
					new Claim(ClaimTypes.Role, role)
				}),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			if (expir != null)
			{
				// Thời gian hết hạn của JWT
				tokenDescriptor.Expires = expir;
			}
			// Thời gian hết hạn của JWT
			else tokenDescriptor.Expires = DateTime.UtcNow.AddMinutes(20);
			var token = tokenHandler.CreateToken(tokenDescriptor);
			var tokenString = tokenHandler.WriteToken(token);
			return tokenString;
		}

		public void RemoveJWTToken(string token, IConfiguration config)
		{
			/*var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(config["AppSettings:SecretKey"]);
			tokenHandler.ValidateToken(token, new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(key),
				ValidateIssuer = false,
				ValidateAudience = false
			}, out var validatedToken);
			var jwtToken = (JwtSecurityToken)validatedToken;
			var username = jwtToken.Claims.First(x => x.Type == "unique_name").Value;
			var role = jwtToken.Claims.First(x => x.Type == "role").Value;
			var UserId = jwtToken.Claims.First(x => x.Type == "nameid").Value;*/
		}
	}
}
