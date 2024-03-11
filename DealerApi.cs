using DMS.BusinessAccessLayer.Interfaces;
using DMS.Components.Modelclasses;
using DMS.Components.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using Transform = DMS.Components.Utilities.Transform;

namespace DMS.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]

    public class DealerApi : Controller
    {
        private readonly IDealerService uService;
        public DealerApi(IDealerService _uService)
        {
            uService = _uService;
        }
        [HttpPost]
        public IActionResult AddBulkDealerTargets(BulkDealerTargetsAddRequest request)
        {
            ApiResponse<BulkUploadDealerTarGetResponse> response = new ApiResponse<BulkUploadDealerTarGetResponse>();
            try
            {
                response = Transform.ConvertResultToApiResonse<BulkUploadDealerTarGetResponse>(uService.AddBulkDealerTargets(request));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpPost]
        public IActionResult SaveBulkDealerTargetsBulk(BulkSalesSaveRequest request)
        {
            ApiResponse<ApiReturnWithResponseFlag> response = new ApiResponse<ApiReturnWithResponseFlag>();
            try
            {
                response = Transform.ConvertResultToApiResonse<ApiReturnWithResponseFlag>(uService.SaveBulkDealerTargetsBulk(request));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpPost]
        public IActionResult AddAssociations(AddAssociationModel request)
        {
            ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
            try
            {
                response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.AddAssociations(request));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpPost]
        public IActionResult AddAssociationProducts(AddAssociationForProductModel request)
        {
            ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
            try
            {
                response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.AddAssociationProducts(request));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpPost]
        public IActionResult GetGeographyDetailsForDealer(RequestForGeoDetailsModel request)
        {
            ApiResponse<List<ReturnModelForAddAssociationDealergeoDetails>> response = new ApiResponse<List<ReturnModelForAddAssociationDealergeoDetails>>();
            try
            {
                //ReturnModelForAddAssociationDealergeoDetails GetGeographyDetailsForDealerInAssociation(RequestForGeoDetailsModel request)
                response = Transform.ConvertResultToApiResonse<List<ReturnModelForAddAssociationDealergeoDetails>>(uService.GetGeographyDetailsForDealerInAssociation(request));
                //response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpPost]
        public IActionResult GetGeographyDetailsForProductInAssociation(RequestForGeoDetailsBasedOnProductModel request)
        {
            ApiResponse<List<ReturnModelForAddAssociationProductgeoDetails>> response = new ApiResponse<List<ReturnModelForAddAssociationProductgeoDetails>>();
            try
            {
                //ReturnModelForAddAssociationDealergeoDetails GetGeographyDetailsForDealerInAssociation(RequestForGeoDetailsModel request)
                response = Transform.ConvertResultToApiResonse<List<ReturnModelForAddAssociationProductgeoDetails>>(uService.GetGeographyDetailsForProductInAssociation(request));
                //response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpPost]
        public IActionResult GetGeosUpToDefaultLevelForDealer(DealerGeoRequestModel req)
        {
            ApiResponse<finalResultUpToDefaultLevel> response = new ApiResponse<finalResultUpToDefaultLevel>();
            try
            {
                response = Transform.ConvertResultToApiResonse<finalResultUpToDefaultLevel>(uService.GetGeosUpToDefaultLevelForDealer(req.GeographyId, req.DealerId));
                //response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpGet]
        public IActionResult GetAddressTypeList()
        {
            ApiResponse<List<AddressType>> response = new ApiResponse<List<AddressType>>();
            try
            {
                response = Transform.ConvertResultToApiResonse<List<AddressType>>(uService.GetAddressTypeList());
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpGet]
        public IActionResult GetAddressTaxList()
        {
            ApiResponse<List<AddressTax>> response = new ApiResponse<List<AddressTax>>();
            try
            {
                response = Transform.ConvertResultToApiResonse<List<AddressTax>>(uService.GetAddressTaxList());
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpPost]
        public IActionResult GetAllDealers(dealerListRequest request)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<AddDealerlist>>(uService.GetDealerList(request));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpGet]
        public IActionResult GetGeographyListdrop()
        {
            ApiResponse<List<GetGeographydrop>> response = new ApiResponse<List<GetGeographydrop>>();
            try
            {
                response = Transform.ConvertResultToApiResonse<List<GetGeographydrop>>(uService.GetGeographydrop());
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpGet]
        public IActionResult GetDealerStatusdrop()
        {
            ApiResponse<List<GetDealerStatusDrop>> response = new ApiResponse<List<GetDealerStatusDrop>>();
            try
            {
                response = Transform.ConvertResultToApiResonse<List<GetDealerStatusDrop>>(uService.GetDealerStatusdrop());
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpPost]
        public IActionResult AddDealer(AddDealerFsModel request)
        {
            ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
            try
            {
                response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.AddDealer(request));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }

        }
        [HttpPost]
        public IActionResult ActiveDeactiveDealer(ActivateDeactivateDealerModel request)
        {
            try
            {


                var response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.ActiveDeactiveDealer(request.CustomerId, request.logedUserId, request.status));
                return Ok(response);

            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpGet]
        public IActionResult GetDealersToEdit(int CustomerId)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<GetDealerFinalModelId>>(uService.GetDealerById(CustomerId));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpPost]
        public IActionResult DeleteAddressDealer(DeleteAddressDealerModel request)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.DeleteAddressDealer(request.AddressId, request.logedUserId));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpPost]
        public IActionResult GetAllDealerAssociation(AssociationListRequest request)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<DealerAssociationList>>(uService.GetAssociationList(request));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        //DealerAssociationList GetAssociationById(GetAssociationByIdModel request)
        [HttpPost]
        public IActionResult GetAssociationById(GetAssociationByIdModel request)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<DealerAssociationList>(uService.GetAssociationById(request));
                //response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpGet]
        public IActionResult GetAssociationGeoDropdown()
        {
            ApiResponse<List<GetAssoGeoDropDown>> response = new ApiResponse<List<GetAssoGeoDropDown>>();
            try
            {
                response = Transform.ConvertResultToApiResonse<List<GetAssoGeoDropDown>>(uService.GetAssoGeographydrop());
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpGet]
        public IActionResult GetAssoDealerdrop()
        {
            ApiResponse<List<GetAssoDealerDropDown1>> response = new ApiResponse<List<GetAssoDealerDropDown1>>();
            try
            {
                response = Transform.ConvertResultToApiResonse<List<GetAssoDealerDropDown1>>(uService.GetAssoDealerdrop());
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpGet]
        public IActionResult GetAssoProdDrop()
        {
            ApiResponse<List<GetAssoProductDropDown>> response = new ApiResponse<List<GetAssoProductDropDown>>();
            try
            {
                response = Transform.ConvertResultToApiResonse<List<GetAssoProductDropDown>>(uService.GetAssoProdDrop());
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpGet]
        public IActionResult GetStockItemDetailList(int ProductSKUId)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<ProductListsModel>>(uService.GetStockItemDetailList(ProductSKUId));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpPost]
        public IActionResult GetDealerBasedonGeog(getDealerbasedgeoRequest req)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<getDealerbasedgeo>>(uService.GetAssoDealerbasedongeo(req));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpGet]
        public IActionResult GetAboveDefaultGeography(int stockitemid)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<AboveDefaultGetGeoModel>>(uService.GetAboveDefaultGeography(stockitemid));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpGet]
        public IActionResult GetAboveDefaultGeographyBasedOnDealer(int dealerId)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<AboveDefaultGetGeoModel>>(uService.GetAboveDefaultGeographyBasedOnDealer(dealerId));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpPost]
        public IActionResult UpdateBulkEditAssociations(BulkAssocReuest1 req)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.UpdateBulkAssociation(req));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }


        [HttpGet]
        public IActionResult GetTargetCount(int targetgroupid)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<TargetCount>(uService.GetTargetCount(targetgroupid));
             
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }


        [HttpGet]
        public IActionResult GetProductListforCount(int targetgroupid)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<productlist>>(uService.GetProductListforCount(targetgroupid));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpPost]
        public IActionResult GetProductGeoCount(geocount request)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<dealers>>(uService.GetProductGeoCount(request));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpPost]

        public IActionResult GetTargetGeographydrop(DealerRequest req)
        {
            ApiResponse<List<GetAssoGeoDropDown>> response = new ApiResponse<List<GetAssoGeoDropDown>>();
            try
            {
                response = Transform.ConvertResultToApiResonse<List<GetAssoGeoDropDown>>(uService.GetTargetGeographydrop(req.CustomerId));

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }

        //[HttpPost]
        //public IActionResult AddTargetDealer(targetModelrequest request)
        //{
        //    try
        //    {
        //        var response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.AddTargetDealer(request));
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex);
        //    }
        //}
        [HttpPost]
        public IActionResult GetAllDealerTargets(targetRequestModel request)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<targetdealerLIstFinal>>(uService.GetDealerTargetList(request));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public IActionResult GetFinancialYear()
        {
            try
            {
                //List<int> yearlist = new List<int>();

                //int currentYear = System.DateTime.Now.Year;
                //for (int i = currentYear-5; i <= currentYear + 1; i++)
                //{
                //    yearlist.Add(i);
                //}

                //var response = Transform.ConvertResultToApiResonse<List<int>>(yearlist);
                //response.totalRecords = response.Response.Count;
                //return Ok(response);
                var response = Transform.ConvertResultToApiResonse<List<FinancialYearModel>>(uService.GetFinancialYears());
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpGet]
        public IActionResult GetDealerTargetById(int TargetAssociationId)
        {

            ApiResponse<targetModelrequestbyId> response = new ApiResponse<targetModelrequestbyId>();
            try
            {
                response = Transform.ConvertResultToApiResonse<targetModelrequestbyId>(uService.GetDealerTargetById(TargetAssociationId));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpPost]       
        public IActionResult UpdateTargetDealer(targetModelupdaterequest request)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.UpdateTargetDealer(request));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpPost]
        public IActionResult AddTargetSDealer(targetModelFinalNrequest req)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.AddTargetSDealer(req));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        
        [HttpPost]
        public IActionResult GetDealerTargetDataById(DealerTargetGetDataRequest request)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<DealerTargetGetDataByIdResponse>(uService.GetDealerTargetDataById(request));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpPost]
        public IActionResult UpdateDealerTargetDataById(DealerTargetGetDataByIdResponse request)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.UpdateDealerTargetDataById(request));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpGet]
        public IActionResult GettargetDealerdropdown(DealerTargetRequest req)
        {
            ApiResponse<List<GetAssoDealerDropDown>> response = new ApiResponse<List<GetAssoDealerDropDown>>();
            try
            {
                response = Transform.ConvertResultToApiResonse<List<GetAssoDealerDropDown>>(uService.GettargetDealerdropdown(req));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }


        [HttpPost]
        public IActionResult GetDealergeonames(dealergeorequest request)
        {
            ApiResponse<List<GetGeoSubDetails>> response = new ApiResponse<List<GetGeoSubDetails>>();
            try
            {
                response = Transform.ConvertResultToApiResonse<List<GetGeoSubDetails>>(uService.GetDealergeonames(request));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpPost]
        public IActionResult GetDealerdropdownByTragetGroupId(DealerTargetRequest req)
        {
            ApiResponse<List<GetAssoDealerDropDown>> response = new ApiResponse<List<GetAssoDealerDropDown>>();
            try
            {
                response = Transform.ConvertResultToApiResonse<List<GetAssoDealerDropDown>>(uService.GettargetDealerdropdownn(req));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
  [HttpPost]
        public IActionResult GetDealerTargetDataByIdRow(DealerTargetGetDataRequestRow request)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<DealerTargetGetDataByIdRowResponse1>(uService.GetDealerTargetDataByIdRow(request));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpPost]
        public IActionResult UpdateDealerTarget(targetModelupdaterequestt request)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.UpdateDealerTarget(request));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpPost]
        public IActionResult BulkUploadEditAssociation(AssociationBulkEditModel request)
        {
            ApiResponse<BulkAssociationAddResponse> response = new ApiResponse<BulkAssociationAddResponse>();

            try
            {
                response = Transform.ConvertResultToApiResonse<BulkAssociationAddResponse>(uService.BulkUploadEditAssociation(request));
                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }
        [HttpPost]
        public IActionResult SaveBulkUploadEditAssociation(BulkSalesSaveRequest request)
        {
            ApiResponse<ApiReturnWithResponseFlag> response = new ApiResponse<ApiReturnWithResponseFlag>();
            try
            {
                response = Transform.ConvertResultToApiResonse<ApiReturnWithResponseFlag>(uService.SaveBulkAssociationUpload(request));
                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }


    }
}