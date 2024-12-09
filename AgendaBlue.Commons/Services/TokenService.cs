using AgendaBlue.Commons.Util;
using AgendaBlue.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web.Mvc;

namespace AgendaBlue.Commons.Services
{
    public class TokenService : Controller
    {
        public static string criarToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var chaveCriptografadaemBytes = Encoding.ASCII.GetBytes(ChaveJWT.ChaveSecreta);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Sid, user.IdUser.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),


                }),
                Expires = DateTime.UtcNow.AddMinutes(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chaveCriptografadaemBytes), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
