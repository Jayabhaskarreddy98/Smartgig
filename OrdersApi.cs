using DMS.BusinessAccessLayer.Interfaces;
using DMS.Components.Modelclasses;
using DMS.Components.Utilities;
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
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersApi : Controller
    {
        private readonly IOrdersService uService;
        public OrdersApi(IOrdersService _uService)
        {
            uService = _uService;
        }
        [HttpGet]
        public IActionResult GetOrdersToEdit(int orderId)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<GetOrderFinalModel>(uService.GetOrderByIdToEdit(orderId));
                //response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpPost]
        public IActionResult GetAllOrders(OrderesRequestModel request)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<OrdersLIstFinal>>(uService.GetOrdersList(request));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpPost]
        public IActionResult GetOrdersListForDownload(OrderesRequestModel request)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<OrdersListDownloadModel>>(uService.GetOrdersListForDownload(request));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        //[HttpPost]
        //public IActionResult GetPricePromotionsList(GetPricePromotionsrequestModel request)
        //{


        //    try
        //    {
        //        var response = Transform.ConvertResultToApiResonse<List<GetPricePromotionsListModel>>(uService.GetPricePromotionsList(request));
        //        response.totalRecords = response.Response.Count;
        //        return Ok(response);
        //    }
        //    catch (Exception e)
        //    {
        //        return Ok(e);
        //    }
        //}

        [HttpPost]
        public IActionResult GetOrderNonPromoList(OrdernonListnonRequest request)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<OrdernonModel>>(uService.GetOrderNonPromotionList(request));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        //[HttpPost]
        //public IActionResult GetBuyGroupOfAPromotion(GetBuyGroupOfAPromotionrequestModel request)
        //{
        //    try
        //    {
        //        var response = Transform.ConvertResultToApiResonse<List<GetBuyGroupOfAPromotionListModel>>(uService.GetBuyGroupOfAPromotion(request));
        //        response.totalRecords = response.Response.Count;
        //        return Ok(response);
        //    }
        //    catch (Exception e)
        //    {
        //        return Ok(e);
        //    }
        //}
        //[HttpPost]
        //public IActionResult GetGetGroupOfAPromotion(GetBuyGroupOfAPromotionrequestModel request)
        //{
        //    try
        //    {
        //        var response = Transform.ConvertResultToApiResonse<List<GetGetGroupOfAPromotionListModel>>(uService.GetGetGroupOfAPromotion(request));
        //        response.totalRecords = response.Response.Count;
        //        return Ok(response);
        //    }
        //    catch (Exception e)
        //    {
        //        return Ok(e);
        //    }
        //}
        [HttpGet]
        public IActionResult GetOredrStatusDropdown()
        {
            ApiResponse<List<OrderStatusDropdown>> response = new ApiResponse<List<OrderStatusDropdown>>();
            try
            {
                response = Transform.ConvertResultToApiResonse<List<OrderStatusDropdown>>(uService.GetOrderStatusdrop());
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpGet]
        public IActionResult GetordergeoDropdown(int CustomerId)
        {
            ApiResponse<List<GetAssoGeoDropDown>> response = new ApiResponse<List<GetAssoGeoDropDown>>();
            try
            {
                response = Transform.ConvertResultToApiResonse<List<GetAssoGeoDropDown>>(uService.GetOrderGeographydrop(CustomerId));

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpGet]
        public IActionResult GetBillingAddress(int Customerid)
        {
            ApiResponse<List<OrderAdress>> response = new ApiResponse<List<OrderAdress>>();
            try
            {
                response = Transform.ConvertResultToApiResonse<List<OrderAdress>>(uService.GetBillingAddress(Customerid));

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }

        }
        [HttpGet]
        public IActionResult GetShipingAddress(int Customerid)
        {
            ApiResponse<List<OrderAdress>> response = new ApiResponse<List<OrderAdress>>();
            try
            {
                response = Transform.ConvertResultToApiResonse<List<OrderAdress>>(uService.GetShipingAddress(Customerid));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }

        }
        [HttpPost]
        public IActionResult GetStockFullDeatils(OrderStockDeatils1 req)
        {
            ApiResponse<List<OrderStockDeatils>> response = new ApiResponse<List<OrderStockDeatils>>();
            try
            {
                response = Transform.ConvertResultToApiResonse<List<OrderStockDeatils>>(uService.GetStockFullDeatils(req));

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpPost]
        public IActionResult AddOrder(AddOrderModel request)
        {
            ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
            try
            {
                response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.AddOrder(request));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        //[HttpGet]
        //public IActionResult GetOrdersToEdit(int CustomerPoId)
        //{
        //    try
        //    {
        //        var response = Transform.ConvertResultToApiResonse<List<GetOrderbyidlist>>(uService.GetOrderById(CustomerPoId));
        //        response.totalRecords = response.Response.Count;
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex);
        //    }
        //}
        [HttpPost]
        public IActionResult OrderConfirmReject(confirmrejectmodel req)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.OrderConfirmReject(req));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpPost]
        public IActionResult AddShipOrder(Shipmodel request)
        {
            ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
            try
            {
                response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.AddShipOrder(request));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }

        [HttpPost]
        public IActionResult AddBulkShipOrder(BulkShipmentAddRequest request)
        {
            ApiResponse<BulkShipmentResponse> response = new ApiResponse<BulkShipmentResponse>();
            try
            {
                response = Transform.ConvertResultToApiResonse<BulkShipmentResponse>(uService.AddBulkShipOrder(request));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }

        public IActionResult AddBulkShipmentNew(BulkShipmentAddRequestNew request)
        {
            ApiResponse<BulkShipmentResponse> response = new ApiResponse<BulkShipmentResponse>();
            try
            {
                response = Transform.ConvertResultToApiResonse<BulkShipmentResponse>(uService.AddBulkShipmentNew(request));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }

        [HttpPost]
        public IActionResult ImportBulkShipOrder(BulkShipmentImportRequest request)
        {
            ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
            try
            {
                response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.ImportBulkShipOrder(request));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }

        [HttpPost]
        public IActionResult GetPromotionListForOrder(PromotionsRequestModelForOrder req)
        {
            //List<promotionListModelForOrders> response = new List<promotionListModelForOrders>();
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<promotionListModelForOrders>>(uService.GetPromotionListForOrder(req));
                response.torecord = response.Response.Count();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpPost]
        public IActionResult GetProductsOfPromotionForOrder(ProMoProdsReqModel promotionId)
        {
            //List<ReturnModelOfpromotionForOrderl> response = new List<ReturnModelOfpromotionForOrderl>();
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<ReturnModelOfpromotionForOrderl>>(uService.GetProductsOfPromotionForOrder(promotionId));

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpPost]
        public IActionResult GetStockFullDeatilsForPromo(OrderStockDetailsForPromo req)
        {
            ApiResponse<List<OrderStockDeatilsForPromo>> response = new ApiResponse<List<OrderStockDeatilsForPromo>>();
            try
            {
                response = Transform.ConvertResultToApiResonse<List<OrderStockDeatilsForPromo>>(uService.GetStockFullDeatilsForPromo(req));

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);

            }

        }


        [HttpPost]
        public IActionResult GetAllShipments(shipmentRequestModel request)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<shipmentLIstFinal>>(uService.GetShipmentList(request));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        public IActionResult GetShippingAndPackingCharges(ShippingRequestModel request)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<ShippingResponseModel>(uService.GetShippingAndPackingCharges(request));
                //response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpPost]
        public IActionResult GetShipUploadList(shipmentuploadRequestModel request)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<shipmentuploadLIstFinal>>(uService.GetshipmentuplodList(request));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }


        [HttpGet]
        public IActionResult GetOrderByship(int orderId)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<GetOrderFinalshipModel>(uService.GetOrderByship(orderId));
                //response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpPost]
        public IActionResult GetOrderReceiptList(OrderReceiptRequest request)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<OrderReceiptListResponse>>(uService.GetOrderReceiptList(request));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public IActionResult GetOrderReceiptDownloadList(OrderReceiptRequest request)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<OrderReceiptDownloadResponse>>(uService.GetOrderReceiptDownloadList(request));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public IActionResult GetShippingAndPackingChargesforship(ShippingRequestModelship request)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<ShippingResponseModel>(uService.GetShippingAndPackingChargesforship(request));
                //response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpPost]
        public IActionResult GetOrderByshipReceiveMain(ReceiveRequest req)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<GetOrderFinalshipReceiveModel>(uService.GetOrderByshipReceiveMain(req));
                //response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpPost]
        public IActionResult GetOrderByshipReceive(ReceiveRequest req)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<GetOrderFinalshipReceiveModel>(uService.GetOrderByshipReceive(req));
                //response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpPost]
        public IActionResult AddReceiveShipOrder(ReceiveShipmodel request)
        {
            ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
            try
            {
                response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.AddReceiveShipOrder(request));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }


        [HttpPost]
        public IActionResult AddBulkOrderReceipt(BulkOrderReceiptUpload request)
        {
            ApiResponse<BulkOrderReceiptResponse> response = new ApiResponse<BulkOrderReceiptResponse>();
            try
            {
                response = Transform.ConvertResultToApiResonse<BulkOrderReceiptResponse>(uService.AddBulkOrderReceipt(request));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }

        [HttpPost]
        public IActionResult SaveBulkOrderReceipt(BulkOrderReceiptSave request)
        {
            ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
            try
            {
                response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.SaveBulkOrderReceipt(request));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpPost]
        public IActionResult GetOrderToView(ViewOrderRequestModel request)
        {
            ApiResponse<GetOrderFinalshipReceiveModel> response = new ApiResponse<GetOrderFinalshipReceiveModel>();
            try
            {
                response = Transform.ConvertResultToApiResonse<GetOrderFinalshipReceiveModel>(uService.GetOrderToView(request));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpPost]
        public IActionResult GetAllStatusdrop(GetstatusDropDownReq req)
        {
            ApiResponse<List<OrderStatusDropdown>> response = new ApiResponse<List<OrderStatusDropdown>>();
            try
            {
                response = Transform.ConvertResultToApiResonse<List<OrderStatusDropdown>>(uService.GetAllStatusdrop(req));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpGet]
        public IActionResult GetProductAndCodeDropdown()
        {
            ApiResponse<List<ProductAndCodeDropdownModel>> response = new ApiResponse<List<ProductAndCodeDropdownModel>>();
            try
            {
                response = Transform.ConvertResultToApiResonse<List<ProductAndCodeDropdownModel>>(uService.GetProductAndCodeDropdown());
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        //ApiReturnWithResponseFlag DeleteShipment(InvoiceReceiveRequest request)
        [HttpPost]
        public IActionResult DeleteShipment(InvoiceReceiveRequest request)
        {
            ApiResponse<ApiReturnWithResponseFlag> response = new ApiResponse<ApiReturnWithResponseFlag>();
            try
            {
                response = Transform.ConvertResultToApiResonse<ApiReturnWithResponseFlag>(uService.DeleteShipment(request));
                //response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        //SPReturnString UpdateOrderStatus(CancleOrderModel request)
        [HttpPost]
        public IActionResult UpdateOrderStatus(CancleOrderModel request)
        {
            ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
            try
            {
                response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.UpdateOrderStatus(request));
                //response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        //List<ShipmentUploadsModel> GetShipmentsUploadList(CommonSearchModel request)
        [HttpPost]
        public IActionResult GetShipmentsUploadList(CommonSearchModel request)
        {
            ApiResponse<List<ShipmentUploadsModel>> response = new ApiResponse<List<ShipmentUploadsModel>>();
            try
            {
                response = Transform.ConvertResultToApiResonse<List<ShipmentUploadsModel>>(uService.GetShipmentsUploadList(request));
                //response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost]
        public IActionResult GetShipmentsUploadViewData(ShipmentUploadsViewData request)
        {
            ApiResponse<List<ShipmentUploadViewModel>> response = new ApiResponse<List<ShipmentUploadViewModel>>();
            try
            {
                response = Transform.ConvertResultToApiResonse<List<ShipmentUploadViewModel>>(uService.GetShipmentsUploadsViewData(request));
                //response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}

