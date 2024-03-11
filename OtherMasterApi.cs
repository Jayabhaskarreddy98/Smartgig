using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class OtherMasterApi : ControllerBase
    {
        private readonly IOtherMasterService uService;
        public OtherMasterApi(IOtherMasterService _uService)
        {
            uService = _uService;
        }
        [HttpPost]
        public IActionResult GetAllUOMs(UoMListRequest request)
        {
            {
                try
                {
                    var response = Transform.ConvertResultToApiResonse<List<UOMList>>(uService.GetUOMList(request.search));
                    response.totalRecords = response.Response.Count;

                    return Ok(response);
                }
                catch (Exception ex)
                {
                    return Ok(ex);
                }
            }
        }
        [HttpPost]
        public IActionResult AddEditUOM(UOMAddEditModel request)
        //            string FirstName, string LastName, string Email, string UserName, string MobilePhone, int RoleId, int Userid = 0)
        {
            try
            {
                ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
                if (request.UoMId > 0)
                {
                    response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.UpdateUOM(request.UoMName, request.UoMShortName, request.UoMId));
                }
                else
                {
                    response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.AddUOM(request.UoMName, request.UoMShortName, request.UoMId));
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpGet]
        public IActionResult GetUOMDetailsToEdit(int UoMId)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<UOMAddEditModelById>(uService.GetUOMDetailsToEdit(UoMId));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpGet]
        public IActionResult DeleteUoM(int UoMId)
        {
            ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
            try
            {
                response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.DeleteUoM(UoMId));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }

        }
        [HttpPost]
        public IActionResult GetCurrencyList(CurrencylistModel request)
        {

            try
            {
                var response = Transform.ConvertResultToApiResonse<List<CurrencyModel>>(uService.GetCurrencysList(request.statuss, request.search));
                response.totalRecords = response.Response.Count;

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }

        }
        [HttpPost]
        public IActionResult AddEditCurrency(CurrecyAddReqModel request)
        {
            try
            {
                ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
                if (request.UoMId > 0)
                {
                    response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.UpdateCurrency(request.UoMName, request.UoMShortName, request.ConversionRate, request.UOMSymbol, request.Conversion, request.UoMId));

                }
                else
                {
                    response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.AddCurrency(request.UoMName, request.UoMShortName, request.ConversionRate, request.UOMSymbol, request.Conversion, request.UoMId));
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpGet]
        public IActionResult GetCurrencyDetailsToEdit(int FxRateId)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<CurrencyModel>(uService.GetCurrencyDetailsToEdit(FxRateId));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpGet]
        public IActionResult GetStatusList()
        {
            ApiResponse<List<StutusListModel>> response = new ApiResponse<List<StutusListModel>>();
            try
            {
                response = Transform.ConvertResultToApiResonse<List<StutusListModel>>(uService.GetSatusList());
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpPost]
        public IActionResult ActiveDeactive(ActivateDeactivateCurrencyModel request)
        {
            try
            {


                var response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.ActiveDeactive(request.UoMId, request.logedUserId, request.status));
                return Ok(response);

            }
            catch (Exception ex)
            {
                return Ok(ex);
            }

        }
            #region Get Details
        //[HttpGet]
        // public IActionResult GetAllCountries()
        //{
        //    ApiResponse<CountryFinalModel> response = new ApiResponse<CountryFinalModel>();
        //    try
        //    {
        //        response = Transform.ConvertResultToApiResonse<CountryFinalModel>(uService.GetAllCountries());
        //        response.totalRecords = response.Response.AllOtherCountries.Count + 1;
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex);

        //    }
        //}
        [HttpGet]
        public IActionResult GetGeographies(int id, int HId)
        {
            ApiResponse<GeographyFinalModel> response = new ApiResponse<GeographyFinalModel>();
            try
            {
                response = Transform.ConvertResultToApiResonse<GeographyFinalModel>(uService.GetGeographies(id,HId));
                response.totalRecords = response.Response.AllOtherGeography.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpGet]
        public IActionResult GetSelectOfMultiGeographies(GeographyIdsModel req)
        {
            ApiResponse<GeographyFinalModel> response = new ApiResponse<GeographyFinalModel>();
            try
            {
                response = Transform.ConvertResultToApiResonse<GeographyFinalModel>(uService.GetSelectOfMultiGeographies(req.GeoId));
                response.totalRecords = response.Response.AllOtherGeography.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpGet]
        public IActionResult GetGeographyHierarchy()
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<GeographyHierarchy>>(uService.GetGeographyHierarchy());
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        #endregion
        #region Delete
        //[HttpPost]
        //public IActionResult DeleteGeography(int Id)
        //{
        //    ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
        //    try
        //    {
        //        response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.DeleteGeography(Id));
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex);

        //    }

        //}
        [HttpGet]
        public IActionResult ActDeAct_Geography(int Id, int logedUserId)
        {
            ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
            try
            {
                response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.ActDeAct_Geography(Id,logedUserId));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }

        }
        #endregion
        #region Add
        //[HttpPost]
        //public IActionResult AddCountry(GeographyModelCountryrequest request)
        //{
        //    ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
        //    try
        //    {
        //        if (request.GeographyName.Trim() != "")
        //        {
        //            response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.AddCountry(request.GeographyName, request.GeographyDesc, request.CreatedById));
        //        }
        //        else
        //        {
        //            response.Response.result = "PLease Enter Name";
        //        }
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex);
        //    }

        //}
        //[HttpPost]
        //public IActionResult AddState(GeographyModelStaterequest request)
        //{
        //    ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
        //    try
        //    {
        //        if (request.GeographyName.Trim() != "")
        //        {
        //            response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.AddState(request.GeographyName, request.GeographyDesc, request.GeographyParentId, request.CreatedById));
        //        }
        //        else
        //        {
        //            response.Response.result = "PLease Enter Name";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex);
        //    }
        //    return Ok(response);
        //}
        //[HttpPost]
        //public IActionResult AddDistrict(GeographyModelStaterequest request)
        //{
        //    ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
        //    try
        //    {
        //        if (request.GeographyName.Trim() != "")
        //        {
        //            response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.AddDistrict(request.GeographyName, request.GeographyDesc, request.GeographyParentId, request.CreatedById));
        //        }
        //        else
        //        {
        //            response.Response.result = "PLease Enter Name";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex);
        //    }
        //    return Ok(response);
        //}
        //[HttpPost]
        //public IActionResult AddCity(GeographyModelStaterequest request)
        //{
        //    ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
        //    try
        //    {
        //        if (request.GeographyName.Trim() != "")
        //        {
        //            response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.AddCity(request.GeographyName, request.GeographyDesc, request.GeographyParentId, request.CreatedById));
        //        }
        //        else
        //        {
        //            response.Response.result = "PLease Enter Name";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex);
        //    }
        //    return Ok(response);
        //}
        //[HttpPost]
        //public IActionResult AddZone(GeographyModelStaterequest request)
        //{
        //    ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
        //    try
        //    {
        //        if (request.GeographyName.Trim() != "")
        //        {
        //            response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.AddZone(request.GeographyName, request.GeographyDesc, request.GeographyParentId, request.CreatedById));
        //        }
        //        else
        //        {
        //            response.Response.result = "PLease Enter Name";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex);
        //    }
        //    return Ok(response);
        //}
        //[HttpPost]
        //public IActionResult AddArea(GeographyModelStaterequest request)
        //{
        //    ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
        //    try
        //    {
        //        if (request.GeographyName.Trim() != "")
        //        {
        //            response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.AddArea(request.GeographyName, request.GeographyDesc, request.GeographyParentId, request.CreatedById));
        //        }
        //        else
        //        {
        //            response.Response.result = "PLease Enter Name";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex);
        //    }
        //    return Ok(response);
        //}
        //[HttpPost]
        //public IActionResult AddSubArea(GeographyModelStaterequest request)
        //{
        //    ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
        //    try
        //    {
        //        if (request.GeographyName.Trim() != "")
        //        {
        //            response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.AddSubArea(request.GeographyName, request.GeographyDesc, request.GeographyParentId, request.CreatedById));
        //        }
        //        else
        //        {
        //            response.Response.result = "PLease Enter Name";
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex);
        //    }
        //    return Ok(response);
        //}
        [HttpPost]
        public IActionResult AddEditGeography(AddEditGeographyFinal respones)
        {
            ApiResponse<ReturnGeographyAddModel> response = new ApiResponse<ReturnGeographyAddModel>();
            try
            {
                response = Transform.ConvertResultToApiResonse<ReturnGeographyAddModel>(uService.AddEditGeography(respones));
                if(response.Response.ResultDetails== "Added Succesfully" || response.Response.ResultDetails == "Updated Succesfully")
                {
                    response.Succeded = true;
                    return Ok(response);
                    //return Ok(true);
                }
                else
                {
                    response.Succeded = false;
                    return NotFound(response);
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex);

            }
        }
        [HttpGet]
        public IActionResult GetGeographyById(int GeographyId)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<GeographyEditModel>(uService.GetGeographyById(GeographyId));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        #endregion
        [HttpPost]
        public IActionResult AddTaxTemplate(TaxTemplateModel respones)
        {
            ApiResponse<string> response = new ApiResponse<string>();
            try
            {
                response = Transform.ConvertResultToApiResonse<string>(uService.AddEditTaxTemplate(respones));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpPost]
        public IActionResult EditTaxTemplate(TaxTemplateModel respones)
        {
            ApiResponse<string> response = new ApiResponse<string>();
            try
            {
                response = Transform.ConvertResultToApiResonse<string>(uService.AddEditTaxTemplate(respones));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpPost]
        public IActionResult GetTAxTemplatesList(TaxTemplateRequestModel request)
        {
            ApiResponse<List<TaxTemplateFinalListModel>> response = new ApiResponse<List<TaxTemplateFinalListModel>>();
            try
            {
                response = Transform.ConvertResultToApiResonse<List<TaxTemplateFinalListModel>>(uService.GetTaxTemplatesList(request));
                response.totalRecords = response.Response.Count();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpGet]
        public IActionResult GetTaxTemplateToEdit(int TaxTemplateId)
        {
            ApiResponse<TaxTemplateEditModel> response = new ApiResponse<TaxTemplateEditModel>();
            try
            {
                response = Transform.ConvertResultToApiResonse<TaxTemplateEditModel>(uService.GetTaxTemplateById(TaxTemplateId));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }

        }
        [HttpGet]
        public IActionResult GetTaxTemplateStatus()
        {
            ApiResponse<List<UserStatusDD>> response = new ApiResponse<List<UserStatusDD>>();
            try
            {
                response = Transform.ConvertResultToApiResonse<List<UserStatusDD>>(uService.GetTaxTemplateStatus());
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpPost]
        public IActionResult DeleteTaxTemplate(int TaxTemplateId)
        {
            ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
            try
            {
                response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.DeleteTaxTemplate(TaxTemplateId));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }


        }
        [HttpGet]
        public IActionResult DeleteTaxTemplateDetails(int TaxTemplateDetailsId)
        {
            ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
            try
            {
                response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.DeleteTaxTemplateDetails(TaxTemplateDetailsId));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpGet]
        public IActionResult TaxTemplateReactivate(int TaxTemplateId)
        {
            ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
            try
            {
                response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.TaxTemplateReactivate(TaxTemplateId));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }


        }
        [HttpGet]
        public IActionResult TaxTemplateDeactivate(int TaxTemplateId)
        {
            ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
            try
            {
                response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.TaxTemplateDeactivate(TaxTemplateId));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }


        }

        [HttpGet]
        public IActionResult GetDefaultCurrency(int CurrentUserId)
        {
            ApiResponse<DefaultCurrency> response = new ApiResponse<DefaultCurrency>();
            try
            {
                response = Transform.ConvertResultToApiResonse<DefaultCurrency>(uService.GetDefaultCurrency(CurrentUserId));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }


    }
}