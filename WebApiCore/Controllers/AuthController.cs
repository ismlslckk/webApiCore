using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace WebApiCore.Controllers
{
    [Route("api/auth")]
    public class AuthController : Controller
    {
        [HttpPost("token")]
        public IActionResult Token()
        {
            var header = Request.Headers["Authorization"];
            if (header.ToString().StartsWith("Basic"))
            {
                var credValue = header.ToString().Substring("Basic ".Length).Trim();
                var usernameAndPassenc = Encoding.UTF8.GetString(Convert.FromBase64String(credValue));
                var usernameAndPass = usernameAndPassenc.Split(":");
                //check in DB username and pass exist

                if (usernameAndPass[0] == "Admin" && usernameAndPass[1] == "pass")
                {
                    var expiresTime = DateTime.Now.AddMinutes(3);

                    var claimsdata = new[] { new Claim(ClaimTypes.Name, usernameAndPass[0]), new Claim(ClaimTypes.Role, "MyAdmin") };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ahbasshfbsahjfbshajbfhjasbfashjbfsajhfvashjfashfbsahfbsahfksdjf"));
                    var signInCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
                    var token = new JwtSecurityToken(
                        issuer: "mysite.com",
                        audience: "mysite.com",
                        expires: DateTime.Now,
                        claims: claimsdata,
                        signingCredentials: signInCred
                    );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                    return Ok(new { token = tokenString, expires = expiresTime });
                }
                else
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "yanlış kullanıcı adı veya şifre.");
                }
            }
            return BadRequest("wrong request");
        }
    }
}