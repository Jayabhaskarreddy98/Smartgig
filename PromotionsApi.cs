using DMS.BusinessAccessLayer.Interfaces;
using DMS.Components.Modelclasses;
using DMS.Components.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DMS.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class PromotionsApi : ControllerBase
    {
        private readonly IPromotionsService uService;
        public PromotionsApi(IPromotionsService _uService)
        {
            uService = _uService;
        }
        [HttpGet]
        public IActionResult GetPromotionTypes()
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<PromotionTypes>>(uService.GetAllPromotioTypes());
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpPost]
        public IActionResult UploadImage(IFormFile promfile)
        {
            string imgName = "";
            if (promfile != null)
            {
                var fileNameUploaded = Path.GetFileName(promfile.FileName);
                if (fileNameUploaded != null)
                {
                    var contentType = promfile.ContentType;
                    string filename = DateTime.UtcNow.ToString();
                    filename = Regex.Replace(filename, @"[\[\]\\\^\$\.\|\?\*\+\(\)\{\}%,;: ><!@#&\-\+\/]", "");
                    filename = Regex.Replace(filename, "[A-Za-z ]", "");
                    filename = "Promotion_" + filename + RandomGenerator.RandomString(4, false);
                    string extension = Path.GetExtension(fileNameUploaded);
                    filename += extension;
                    var uploads = Path.Combine("UploadedImages");
                    var filePath = Path.Combine(uploads, filename);
                    promfile.CopyTo(new FileStream(filePath, FileMode.Create));
                    imgName = filename;
                    //isProductImageUploaded = true;
                }
                else
                {
                    imgName = "dummy.jpg";
                }
            }
            else
            {
                imgName = "dummy.jpg";
            }
            ApiResponse<string> response = new ApiResponse<string>();
            try
            {
                response = Transform.ConvertResultToApiResonse<string>(imgName);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpPost]
        public IActionResult GetProductList(PromotionsListRequest request)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<ProductListsModel>>(uService.GetProductList(request.category,request.subCategory,request.type,request.productgroup,request.productSubGroup,request.productidentifier,request.search));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpPost]
        public IActionResult GetAddProduct(AddProductListRequest request)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<AddProductModel>>(uService.GetAddProduct(request.Id));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpPost]
        public IActionResult GetProductGroupList(PromotionsListRequest request)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<ProductGroupListModel>>(uService.GetProductGroupList(request.search));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpGet]
        public IActionResult GetPGDetailList(int ProductGroupId)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<ProductListsModel>>(uService.GetPGDetailList(ProductGroupId));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpPost]
        public IActionResult GetAddProductGroup(AddProductListRequest request)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<AddProductModel>>(uService.GetAddProductGroup(request.Id));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpPost]
        public IActionResult GetProductSubGroupList(PromotionsListRequest request)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<ProductSubGroupListModel>>(uService.GetProductSubGroupList(request.productgroup));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpGet]
        public IActionResult GetPSGDetailList(int ProductGroupId, int ProductSubGroupId)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<ProductListsModel>>(uService.GetPSGDetailList(ProductGroupId, ProductSubGroupId));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpPost]
        public IActionResult GetAddProductSubGroup(AddProductListRequest request)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<AddProductModel>>(uService.GetAddProductSubGroup(request.Id));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpPost]
        public IActionResult GetProductShortCodeList(PromotionsListRequest request)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<ProductShortCodeListModel>>(uService.GetProductShortCodeList(request.search));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpGet]
        public IActionResult GetPSCDetailList(string ProductShortCode)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<ProductListsModel>>(uService.GetPSCDetailList(ProductShortCode));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpPost]
        public IActionResult GetAddProductShortCode(AddProductShortCodeListRequest request)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<AddProductModel>>(uService.GetAddProductShortCode(request.ProductShortCode));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpPost]
        public IActionResult AddEditPromotions(AddEditPromotionsFinal respones)
        {
            ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
            try
            {
                response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.AddEditPromotions(respones));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpPost]
        public IActionResult AddEditBuy_APBGet_XPY(AddEditBuy_APBGet_XPYFinal respones)
        {
            ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
            try
            {
                response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.AddEditBuy_APBGet_XPY(respones));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpPost]
        public IActionResult AddEditOptionalPromotions(AddEditBuyGetOptionalFinal respones)
        {
            ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
            try
            {
                response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.AddEditOptionalPromotions(respones));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpPost]
        public IActionResult AddEditPriceDiscount(AddEditPriceDiscountFinal respones)
        {
            ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
            try
            {
                response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.AddEditPriceDiscount(respones));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpPost]
        public IActionResult AddEditVolumeDiscount(AddEditVolumeDiscountFinal respones)
        {
            ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
            try
            {
                response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.AddEditVolumeDiscount(respones));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpGet]
        public IActionResult GetProductIdentifier()
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<ProductIdentifier>>(uService.GetProductIdentifier());
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpGet]
        public IActionResult GetGeographies()
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<Geographies>>(uService.GetGeographies());
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpGet]
        public IActionResult GetPromotionSatusList()
        {
            ApiResponse<List<StutusListModel>> response = new ApiResponse<List<StutusListModel>>();
            try
            {
                response = Transform.ConvertResultToApiResonse<List<StutusListModel>>(uService.GetPromotionSatusList());
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        //[HttpPost]
        //public IActionResult GetPromotionList(PromotionListRequest request)
        //{
        //    try
        //    {
        //        var response = Transform.ConvertResultToApiResonse<List<PromotionListsModel>>(uService.GetPromotionList(request.promotiontype, request.product, request.geography, request.dealer, request.status, request.startDate, request.endDate, request.search));
        //        response.totalRecords = response.Response.Count;
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex);
        //    }
        //}
        [HttpPost]
        public IActionResult GetPromotionList(PromotionListModel request)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<PromotionListsModel>>(uService.GetPromotionList(request));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpPost]
        public IActionResult GetPromotionDealerList(PromotionsDealerListRequest request)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<PromotionDealerListModel>>(uService.GetPromotionDealerList(request));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpGet]
        public IActionResult GetDealersDropDown()
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<Dealers>>(uService.GetDealersDropDown());
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpGet]
        public IActionResult PromotionClose(int ProductPromotionsId)
        {
            ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
            try
            {
                response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.PromotionClose(ProductPromotionsId));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpGet]
        public IActionResult GetPromotionByIdToEdit(int promotionId)
        {

            ApiResponse<ReturnModelOfpromotionFinal> response = new ApiResponse<ReturnModelOfpromotionFinal>();
            try
            {
                response = Transform.ConvertResultToApiResonse<ReturnModelOfpromotionFinal>(uService.GetPromotionByIdToEdit(promotionId));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpGet]
        public IActionResult GetAboveDefaultGeographyforPromotion()
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<AboveDefaultGetGeoModel>>(uService.GetAboveDefaultGeographyforPromotion());
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpPost]
        public IActionResult GetPromotionByIdToView(RequestViewPromoModel req)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<ReturnViewModelOfpromotionFinal>(uService.GetPromotionByIdToView(req.PromotionId));
                //response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpPost]
        public IActionResult GetSubProducts(ProdSubGroupRequestModel req)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<ProductListModel>>(uService.GetSubProducts(req));
                //response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
    }
    //List<ProductListModel> GetSubProducts(ProdSubGroupRequestModel req)
}
