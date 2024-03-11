using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DMS.Api.AuthServices;
using DMS.BusinessAccessLayer.Interfaces;
using DMS.Components.Entities;
using DMS.Components.Modelclasses;
using DMS.Components.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DMS.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtSettings jwtSettings;
        private readonly IUserMgmtService _uService;


        public AccountController(IUserMgmtService uService,JwtSettings jwtSettings)
        {
            this._uService = uService;
            this.jwtSettings = jwtSettings;
        }
        //private IEnumerable<UserMasterEntity> logins = new List<UserMasterEntity>() {
        //    new UserMasterEntity() {
        //            UserId = Guid.NewGuid(),
        //                Email = "adminakp@gmail.com",
        //                UserName = "Admin",
        //                Password = "Admin",
        //        },
        //        new User() {
        //            Id = Guid.NewGuid(),
        //                EmailId = "adminakp1@gmail.com",
        //                UserName = "User1",
        //                Password = "Admin",
        //        }
        //};
        [Consumes("application/x-www-form-urlencoded")]
        [HttpPost]
        public IActionResult GetToken()
        {
            try
            {
                //UserLogins userLogins = new UserLogins();
                var request = HttpContext.Request;
                string userName = request.Form["emailId"].ToString();
                string pword = request.Form["password"].ToString();
                var Token = new UserTokens();
                // validate user with username and password 
                //LoginRequestModel lRequest = new LoginRequestModel();
                //lRequest.emailId = userName;
                //lRequest.pword = pword;
                var resp = _uService.LoginCheck(userName,pword);

                //  var Valid = logins.Any(x => x.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));
                if (resp.statusCode==1)
                {
                    Token = JwtHelpers.GenTokenkey(new UserTokens()
                    {
                        GuidId = Guid.NewGuid(),
                        UserName = resp.DisplayName,
                        UserType = resp.RoleName,
                        Id = resp.userId,
                        LastLoginDate=resp.LastLoginDate,
                        RoleId=resp.RoleId,
                        Image=resp.Image
                    }, jwtSettings);
                }
                //if (resp.statusCode == 1)
                //{
                //    var user = logins.FirstOrDefault(x => x.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));
                //    Token = JwtHelpers.GenTokenkey(new UserTokens()
                //    {
                //        EmailId = resp.emailId,
                //        GuidId = Guid.NewGuid(),
                //        UserName = resp.userName,
                //        UserType = resp.userTypeName,
                //        Id = resp.userId,
                //    }, jwtSettings);
                //}
                else
                {
                    return BadRequest(resp.statusMessage);
                }
                return Ok(Token);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// Get List of UserAccounts
        /// </summary>
        /// <returns>List Of UserAccounts</returns>
        //[HttpGet]
        //[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        //public IActionResult GetList()
        //{
        //  return Ok(logins);
        //}

        [HttpPost]
        public IActionResult ForgotPassword(ForgotPasswordModel request)
        {
            try
            {
                string result = _uService.ForgotPassword(request.username);

                if (result.ToUpper().Contains("ERROR"))
                {
                    string errors = result.ToUpper().Replace("ERROR: ", "");
                    var response = Transform.GetErrorResponse<string>(result, errors);                    
                    return NotFound(response);
                }
                else
                {
                    var response = Transform.ConvertResultToApiResonse<string>(result);
                    return Ok(response);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost]
        public IActionResult ResetUserPassword(ForgotPasswordModel request)
        {
            try
            {
                string result = _uService.ResetUserPassword(request.token, request.password);

                if (result.ToUpper().Contains("ERROR"))
                {
                    string errors = result.ToUpper().Replace("ERROR: ", "");
                    var response = Transform.GetErrorResponse<string>(result, errors);
                    return NotFound(response);
                }
                else
                {
                    var response = Transform.ConvertResultToApiResonse<string>(result);
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
