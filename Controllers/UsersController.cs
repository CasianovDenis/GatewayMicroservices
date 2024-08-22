using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Myproject.Base.Models;
using Myproject.Services;
using Newtonsoft.Json;
using Myproject.Models.Controller.User;
using Myproject.Models.Controller.Controllser;

namespace Myproject.Controllers
{
    [Route("user")]
    [ApiController]
    public class UsersController : PPController
    {
        private readonly IConfiguration _configuration;
        private readonly HttpService _httpService = new HttpService();

        public UsersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("create_account")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult CreateAccount(CreateUser user)
        {
            var result = new ResultBase() { ResponseCode = ResponseCode.SUCCES };
            try
            {
                var url = _configuration.GetSection("UrlPath:User_Create");

                var response = _httpService.PostRequest(user, url.Value);
                if (!response.IsOk)
                    throw new Error(response.ResponseCode, response.ResultMessage);

                result = JsonConvert.DeserializeObject<ResultBase>(response.ReturnObject);
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
            return Ok(result);
        }

        [Route("get_profile_info")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public IActionResult GetProfileInfo()
        {
            var result = new Result<UserInfoResponse>() { ResponseCode = ResponseCode.SUCCES };
            try
            {
                var url = _configuration.GetSection("UrlPath:User_GetInfo");

                var response = _httpService.GetRequest(url.Value, AcessToken);

                result.ReturnObject = JsonConvert.DeserializeObject<UserInfoResponse>(response.ReturnObject);
                if (!response.IsOk)
                    throw new Error(response.ResponseCode, response.ResultMessage);
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

        [Route("modify_profile")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        public IActionResult ModifyProfile(ModifyUser user)
        {
            var result = new ResultBase() { ResponseCode = ResponseCode.SUCCES };
            try
            {
                var url = _configuration.GetSection("UrlPath:User_ModifyProfile");

                var response = _httpService.PutRequest(user, url.Value, AcessToken);
                if (!response.IsOk)
                    throw new Error(response.ResponseCode, response.ResultMessage);

                result = JsonConvert.DeserializeObject<ResultBase>(response.ReturnObject);
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
            return Ok(result);
        }

        [Route("modify_password")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        public IActionResult ModifyPassword(ModifyUser user)
        {
            var result = new ResultBase() { ResponseCode = ResponseCode.SUCCES };
            try
            {
                var url = _configuration.GetSection("UrlPath:User_ModifyPass");

                var response = _httpService.PutRequest(user, url.Value, AcessToken);
                if (!response.IsOk)
                    throw new Error(response.ResponseCode, response.ResultMessage);

                result = JsonConvert.DeserializeObject<ResultBase>(response.ReturnObject);
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
            return Ok(result);
        }

        [Route("close_account")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete]
        public IActionResult CloseAccount()
        {
            var result = new ResultBase() { ResponseCode = ResponseCode.SUCCES };
            try
            {
                var url = _configuration.GetSection("UrlPath:User_CloseAccount");

                var response = _httpService.DeleteRequest(url.Value, AcessToken);
                if (!response.IsOk)
                    throw new Error(response.ResponseCode, response.ResultMessage);

                result = JsonConvert.DeserializeObject<ResultBase>(response.ReturnObject);
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
            return Ok(result);
        }
    }


}
