using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Todo.Api.DTO;
using Todo.Api.Contracts;
using Todo.Domain.Entities;

namespace Todo.Api.Utils;

public class TokenService : ITokenService
{
    public byte[] SecretKey { get; set; }

    public TokenService(byte[] secretKey)
    {
        SecretKey = secretKey;
    }

    public TokenResult GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
            }),
            Expires = DateTime.UtcNow.AddHours(4),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(SecretKey), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new TokenResult(tokenHandler.WriteToken(token));
    }
}
