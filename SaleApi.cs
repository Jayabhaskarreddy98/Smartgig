using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DMS.BusinessAccessLayer.Interfaces;
using DMS.Components.Entities;
using DMS.Components.Modelclasses;
using DMS.Components.Utilities;
//using ExcelDataReader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Net;

namespace DMS.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class SaleApi : ControllerBase
    {
        private readonly ISaleService uService;
        public SaleApi(ISaleService _uService)
        {
            uService = _uService;
        }

        [HttpPost]
        public IActionResult SalesReportLists(SalesReportsReqModel request)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<ReportsListDinamicModel>(uService.SalesReportLists(request));
                response.totalRecords = response.Response.gridData.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpGet]
        public IActionResult GetProductSales(int StockItemId)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<SalesProductsModel>(uService.GetProductSales(StockItemId));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpPost]
        public IActionResult AddSaleOrder(AddSale request)
        {
            ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
            try
            {
                response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.AddSaleOrder(request));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }


        [HttpPost]
        public IActionResult AddBulkSaleOrder(BulkSalesAddRequest request)
        {
            ApiResponse<BulkSalesAddResponse> response = new ApiResponse<BulkSalesAddResponse>();
            try
            {
                response = Transform.ConvertResultToApiResonse<BulkSalesAddResponse>(uService.AddBulkSaleOrder(request));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }

        [HttpPost]
        public IActionResult SaveBulkSaleOrder(BulkSalesSaveRequest request)
        {
            ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
            try
            {
                response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.SaveBulkSaleOrder(request));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }

        [HttpPost]
        public IActionResult GetSalesById(ViewSaleRequest request)
        {
            ApiResponse<ViewSaleResponse> response = new ApiResponse<ViewSaleResponse>();
            try
            {
                if (!(request.DealerId > 0 && request.GeographyId > 0 && request.ProductId > 0))
                    throw new Exception("Invalid Parameters Value");
                response = Transform.ConvertResultToApiResonse<ViewSaleResponse>(uService.GetSalesById(request));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public IActionResult GetProductdropbyDealerid(ProductRequest request)
        {
            ApiResponse<List<ProductModel>> response = new ApiResponse<List<ProductModel>>();
            try
            {
                response = Transform.ConvertResultToApiResonse<List<ProductModel>>(uService.GetProductdropbyDealerid(request));

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }

        }
        [HttpPost]
        public IActionResult GetSalesList(SalesListModel request)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<SalesLIstFinal>>(uService.GetSalesList(request));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpPost]
        public IActionResult GetSalesUploadList(SalesUploadListRequest request)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<SalesUploadList>>(uService.GetSalesUploadList(request));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpPost]
        public IActionResult GetSalesUploadDetail(SalesUploadListDetailRequest request)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<SalesUploadDetailResponse>>(uService.GetSalesUploadDetail(request));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }



        [HttpGet]
        public IActionResult GetDealersDropdown(int CurrentUserId)
        {
            ApiResponse<List<Customermodel>> response = new ApiResponse<List<Customermodel>>();
            try
            {
                response = Transform.ConvertResultToApiResonse<List<Customermodel>>(uService.GetDealersDropdown(CurrentUserId));

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }

        }
        [HttpPost]
        public IActionResult Getgeobasedondealer(dealerRequest req)
        {
            ApiResponse<List<geographymodel>> response = new ApiResponse<List<geographymodel>>();
            try
            {
                response = Transform.ConvertResultToApiResonse<List<geographymodel>>(uService.Getgeobasedondealer(req));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpPost]
        public IActionResult GetMProductdropbyDealeridGeoid(ProductRequestS req)
        {
            ApiResponse<List<ProductModel>> response = new ApiResponse<List<ProductModel>>();
            try
            {
                response = Transform.ConvertResultToApiResonse<List<ProductModel>>(uService.GetMProductdropbyDealeridGeoid(req));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }

        }
        [HttpGet]
        public IActionResult GetInvoicetranscationDropdown(int CurrentUserId)
        {
            ApiResponse<List<reportmodel>> response = new ApiResponse<List<reportmodel>>();
            try
            {
                response = Transform.ConvertResultToApiResonse<List<reportmodel>>(uService.GetInvoicetranscationDropdown(CurrentUserId));

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }

        }


    }

}
