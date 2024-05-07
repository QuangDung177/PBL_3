using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyProjectName.Utility;
using MyProjectName.Manager.Interface;
using log4net;
using Newtonsoft.Json;

namespace MyProjectName.API.Controllers
{
    [ApiController]
    public class TokenController : ControllerBase
    {
        ILog log4Net;
        private readonly AppSettings _appSettings;
        public TokenController(IOptions<AppSettings> appSettings)
        {
            log4Net = this.Log<TokenController>();
            _appSettings = appSettings.Value;
        }
        [HttpPost]
        [Route(APIEndpoint.DefaultRoute)]
        public ActionResult Post(AuthenticateModel model)
        {
            try
            {
                //Note: Implement your own logic to get username/passoward and validate
                var defaultUsername = _appSettings.DefaultTokenUsername;
var defaultPassword = _appSettings.DefaultTokenPassword;
if (model.Username == defaultUsername && model.Password == defaultPassword)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                    var day = _appSettings.TokenValidityDay;
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Name,model.Username),
                            new Claim(ClaimTypes.Name, "CustomObjectCanBeAddedHere")
                        }),
                        Expires = DateTime.UtcNow.AddDays(day),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    if (token != null)
                    {
                        TokenResponse tokenResponse = new TokenResponse()
                        {
                            AccessToken = tokenHandler.WriteToken(token),
                            ValidFrom = token.ValidFrom,
                            ValidTo = token.ValidTo
                        };

                        return Ok(new APIResponse(ResponseCode.SUCCESS, "Bearer Token Generated", tokenResponse));
                    }

                }

                return StatusCode(401, new APIResponse(ResponseCode.ERROR, "Invalid request"));

            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, ex.Message));
            }
        }
    }
    public class TokenResponse
    {
        public string AccessToken { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
    }
}

