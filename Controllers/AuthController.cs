using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Myproject.Base.Models;
using Myproject.Services;
using Newtonsoft.Json;
using Myproject.Models.Controller.Auth;

namespace Myproject.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : PPController
    {
        private readonly IConfiguration _configuration;
        private readonly HttpService _httpService = new HttpService();

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                authClaims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: signIn);

            return token;
        }

        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(LoginRequest user)
        {
            var result = new Result<LoginResponse>() { ResponseCode = ResponseCode.SUCCES, ReturnObject = new LoginResponse() };
            try
            {
                var url = _configuration.GetSection("UrlPath:Login");

                var response = _httpService.PostRequest(user, url.Value);
                if (!response.IsOk)
                    throw new Error(response.ResponseCode, response.ResultMessage);

                result.ReturnObject = JsonConvert.DeserializeObject<LoginResponse>(response.ReturnObject);
            }
            catch (Error error)
            {
                result.ResponseCode = error.Code;
                result.ResultMessage = error.Message;
            }
            catch (Exception ex)
            {
                result.ResponseCode = ResponseCode.TECHNICAL_EXCEPTION;
                result.ResultMessage = ex.Message;
            }

            return Ok(result.ReturnObject);
        }
    }
}
