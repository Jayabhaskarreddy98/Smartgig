using DMS.BusinessAccessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMS.Api.Controllers
{
    
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class JobController : ControllerBase
    {
        private readonly IBackgroundJobsService bService;
        public JobController(IBackgroundJobsService _bService)
        {
            bService = _bService;
        }
        [HttpPost]
        public IActionResult RunMinutelyJob()
        {
            bService.RunMinutelyJob();
            return Ok("Minutely job triggered manually.");
        }
    }
}
