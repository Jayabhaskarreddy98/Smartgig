using DMS.BusinessAccessLayer.Interfaces;
using DMS.Components.Modelclasses;
using DMS.Components.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMS.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class CommonApiController : ControllerBase
    {
        private readonly ICommonService cService;
        public CommonApiController(ICommonService _cService)
        {
            cService = _cService;
        }
        [HttpPost]
        public IActionResult GetHeirarchyOfDefaultGeography(GeographiesMaterialReqModel geoId)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<finalResultOfGeoForAddMaterial> (cService.GetGeoWithUpperLevel(geoId));
                //response.totalRecords = response.Response.Count;

                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }
        public IActionResult GetGeographiesUpToDefaultLevel(int geoId)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<finalResultUpToDefaultLevel> (cService.GetGeographiesUpToDefaultLevel(geoId));
                //response.totalRecords = response.Response.Count;

                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }
        [HttpGet]
        public IActionResult GetDealerId(int CurrentUserId)
        {          
            try
            {
               var response = Transform.ConvertResultToApiResonse<Dealer>(cService.GetDealerId(CurrentUserId));
                if (response.Response.DealerId > 0)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest("please send correct user id ");
                }
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        //List<SampleExcelModel> GetSampleTemplates(ExcelRequestModel request)
        [HttpPost]
        public IActionResult GetSampleTemplates(ExcelRequestModel request)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<SampleExcelModel>>(cService.GetSampleTemplates(request));
                
                    return Ok(response);                
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        //[HttpPost]
        //public IActionResult GetAllOrders(OrderesRequestModel request)
        //{
        //    try
        //    {
        //        var response = Transform.ConvertResultToApiResonse<List<OrdersLIstFinal>>(uService.GetOrdersList(request));
        //        response.totalRecords = response.Response.Count;
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex);
        //    }
        //}
    }
}
