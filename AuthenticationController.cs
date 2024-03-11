using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DMS.BusinessAccessLayer.Interfaces;
using DMS.Components.Utilities;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DMS.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserMgmtService uService;
        public AuthenticationController(IUserMgmtService _uService)
        {
            uService = _uService;
        }
        //[HttpGet]
        //public IActionResult Login(String userName, string password)
        //{
        //    try
        //    {
        //        var response = Transform.ConvertResultToApiResonse<int>(uService.LoginCheck(userName, password));
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex);
        //    }
        //}

    }
}
