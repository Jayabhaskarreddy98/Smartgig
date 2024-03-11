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
    public class MaterialApi : Controller
    {
        private readonly IMaterialService uService;
        private readonly IHostingEnvironment hostingEnvironment;
        public MaterialApi(IMaterialService _uService, IHostingEnvironment _host)
        {
            uService = _uService;
            hostingEnvironment = _host;
        }
        [HttpPost]
        public IActionResult GetGeographyForMaterial(MaterialGeoRequestModel request)
        {
            ApiResponse<finalResultUpToDefaultLevel> response = new ApiResponse<finalResultUpToDefaultLevel>();
            try
            {
                response = Transform.ConvertResultToApiResonse<finalResultUpToDefaultLevel>(uService.GetGeosUpToDefaultLevelForStockitems(request.GeographyId, request.StockItemId));

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpPost]
        public IActionResult AddCategory(MaterialModelCategoryrequest request)
        {
            ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
            try
            {
                if (request.CategoryName.Trim() != "")
                {
                    response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.AddCategory(request.CategoryName, request.CategoryCode, request.CreatedById));
                }
                else
                {
                    response.Response.result = "PLease Enter Name";
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }

        }
        [HttpPost]
        public IActionResult AddSubCategory(MaterialsubCategoryrequest request)
        {
            ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
            try
            {
                if (request.subCategoryName.Trim() != "")
                {

                    response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.AddsubCategory(request.subCategoryName, request.subCategoryCode, request.categoryid, request.CreatedById));

                }
                else
                {
                    response.Response.result = "PLease Enter Name";
                }

            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
            return Ok(response);

        }
        [HttpPost]
        public IActionResult Addstocktype(Materialstocktyperequest request)
        {
            ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
            try
            {
                if (request.typeName.Trim() != "")
                {

                    response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.Addstocktype(request.typeName, request.typeCode, request.subcategoryid, request.CreatedById));

                }
                else
                {
                    response.Response.result = "PLease Enter Name";
                }

            }

            catch (Exception ex)
            {
                return Ok(ex);

            }
            return Ok(response);
        }
        [HttpGet]
        public IActionResult DeleteCategory(int CategoryId)
        {
            ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
            try
            {
                response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.DeleteCategory(CategoryId));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }

        }
        [HttpGet]
        public IActionResult DeletesubCategory(int subCategoryId)
        {
            ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
            try
            {
                response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.DeletesubCategory(subCategoryId));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }

        }
        [HttpGet]
        public IActionResult Deletetype(int typeId)
        {
            ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
            try
            {
                response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.Deletetype(typeId));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }

        }
        [HttpGet]
        public IActionResult GetCategories(bool flag = false)
        {
            ApiResponse<CatFinalModel> response = new ApiResponse<CatFinalModel>();
            try
            {
                response = Transform.ConvertResultToApiResonse<CatFinalModel>(uService.GetCategories(flag));
                response.totalRecords = response.Response.AllOtherCats.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpGet]
        public IActionResult GetSUbCAts(int catid, bool flag = false)
        {
            ApiResponse<SubCatFinalMOdel> response = new ApiResponse<SubCatFinalMOdel>();
            try
            {
                response = Transform.ConvertResultToApiResonse<SubCatFinalMOdel>(uService.GetSUbCAts(catid, flag));
                response.totalRecords = response.Response.AllOtherSubCAts.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpGet]
        public IActionResult GetTypes(int subCatId, bool flag = false)
        {
            ApiResponse<List<TypeModel>> response = new ApiResponse<List<TypeModel>>();
            try
            {
                response = Transform.ConvertResultToApiResonse<List<TypeModel>>(uService.GetTypes(subCatId, flag));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpPost]
        public IActionResult AddMaterial(addeditmaterialFinal respones)
        {
            ApiResponse<ApiReturnWithResponseFlag> response = new ApiResponse<ApiReturnWithResponseFlag>();
            try
            {
                response = Transform.ConvertResultToApiResonse<ApiReturnWithResponseFlag>(uService.AddEditMaterial(respones));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpPost]
        public IActionResult EditMaterial(addeditmaterialFinal respones)
        {
            ApiResponse<ApiReturnWithResponseFlag> response = new ApiResponse<ApiReturnWithResponseFlag>();
            try
            {
                response = Transform.ConvertResultToApiResonse<ApiReturnWithResponseFlag>(uService.AddEditMaterial(respones));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpPost]
        public IActionResult UploadImg(IFormFile prodfile)
        {
            string imgName = "";
            if (prodfile != null)
            {
                var fileNameUploaded = Path.GetFileName(prodfile.FileName);
                if (fileNameUploaded != null)
                {
                    var contentType = prodfile.ContentType;
                    string filename = DateTime.UtcNow.ToString();
                    filename = Regex.Replace(filename, @"[\[\]\\\^\$\.\|\?\*\+\(\)\{\}%,;: ><!@#&\-\+\/]", "");
                    filename = Regex.Replace(filename, "[A-Za-z ]", "");
                    filename = "Product_" + filename + RandomGenerator.RandomString(4, false);
                    string extension = Path.GetExtension(fileNameUploaded);
                    filename += extension;
                    var uploads = Path.Combine("UploadedImages");
                    var filePath = Path.Combine(uploads, filename);
                    prodfile.CopyTo(new FileStream(filePath, FileMode.Create));
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
        [HttpGet]
        public IActionResult GetMaterialByIdToEdit(int StockItemId)
        {
            ApiResponse<materialGetFinalModel> response = new ApiResponse<materialGetFinalModel>();
            try
            {
                response = Transform.ConvertResultToApiResonse<materialGetFinalModel>(uService.GetStockItemInfoByStockItemId(StockItemId));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }

        }
        [HttpGet]
        public IActionResult GetMAterialIdentifier()
        {
            ApiResponse<List<MaterialIdentifierListModel>> response = new ApiResponse<List<MaterialIdentifierListModel>>();
            try
            {
                response = Transform.ConvertResultToApiResonse<List<MaterialIdentifierListModel>>(uService.GetMaterialIdentifierLists());
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpGet]
        public IActionResult GetProductGroupList()
        {
            ApiResponse<List<ProductListModel>> response = new ApiResponse<List<ProductListModel>>();
            try
            {
                response = Transform.ConvertResultToApiResonse<List<ProductListModel>>(uService.GetPAroductGroupList());
                response.totalRecords = response.Response.Count;
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
        public IActionResult AddProductInformation(AddeditProductInformation respones)
        {

            ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
            try
            {
                response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.AddEditProductInformation(respones));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpGet]
        public IActionResult GetSubProducts(int ParentId)
        {
            ApiResponse<List<ProductListModel>> response = new ApiResponse<List<ProductListModel>>();
            try
            {
                response = Transform.ConvertResultToApiResonse<List<ProductListModel>>(uService.GetSubProductGroupList(ParentId));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }

        }
        [HttpGet]
        public IActionResult GetProductCustomIdentifier()
        {
            ApiResponse<List<ProductCustomerIdentifierReturnModel>> response = new ApiResponse<List<ProductCustomerIdentifierReturnModel>>();
            try
            {
                response = Transform.ConvertResultToApiResonse<List<ProductCustomerIdentifierReturnModel>>(uService.GetProductCustomIdentifierLists());
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpPost]
        public IActionResult GetMaterailList(MaterialListRequest request)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<MaterialListModel>>(uService.GetMaterialList(request.Cat, request.Sub_Cat, request.type, request.product, request.status, request.search, request.isProduct));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpGet]
        public IActionResult GetStockList()
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<StockListModel>>(uService.GetStockList());
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpPost]
        public IActionResult GetSUbCAtsOfMultiCats(CatIdsModel req)
        {
            ApiResponse<SubCatFinalMOdel> response = new ApiResponse<SubCatFinalMOdel>();
            try
            {
                response = Transform.ConvertResultToApiResonse<SubCatFinalMOdel>(uService.GetSUbCAtsOfMultiCats(req.catId));
                response.totalRecords = response.Response.AllOtherSubCAts.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpPost]
        public IActionResult GettypesOfMultiSubCats(SubCatIdsModel req)
        {
            ApiResponse<List<TypeModel>> response = new ApiResponse<List<TypeModel>>();
            try
            {
                response = Transform.ConvertResultToApiResonse<List<TypeModel>>(uService.GetTypesOfMultiSubCats(req.subCatId));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpPost]
        public IActionResult AddProductGroup(ProductgroupAddModel request)
        {

            ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
            try
            {
                response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.AddProductGroup(request));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }

        }
        [HttpPost]
        public IActionResult AddSubProductGroup(ProductSubgroupAddModel request)
        {

            ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
            try
            {
                response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.AddSubProductGroup(request));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }

        }
        [HttpGet]
        public IActionResult MaterialReactivate(int StockItemId)
        {
            ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
            try
            {
                response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.MaterialReactivate(StockItemId));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }


        }
        [HttpGet]
        public IActionResult MaterialDeactivate(int StockItemId)
        {
            ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
            try
            {
                response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.MaterialDeactivate(StockItemId));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }


        }
        [HttpPost]
        public IActionResult ActiveDeactiveCategory(ActivateDeactivateCategory request)
        {
            try
            {


                var response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.ActiveDeactiveCategory(request.CategoryId, request.logedUserId, request.status));
                return Ok(response);

            }
            catch (Exception ex)
            {
                return Ok(ex);
            }

        }
        [HttpPost]
        public IActionResult ActiveDeactiveSubCategory(ActivateDeactivateSubCategory request)
        {
            try
            {


                var response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.ActiveDeactiveSubCategory(request.SubCategoryId, request.logedUserId, request.status));
                return Ok(response);

            }
            catch (Exception ex)
            {
                return Ok(ex);
            }

        }
        [HttpPost]
        public IActionResult ActiveDeactiveType(ActivateDeactivateType request)
        {
            try
            {


                var response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.ActiveDeactiveType(request.TypeId, request.logedUserId, request.status));
                return Ok(response);

            }
            catch (Exception ex)
            {
                return Ok(ex);
            }

        }
        [HttpGet]
        public IActionResult GetCategoryById(int CategoryId)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<CategoryByIdModel>(uService.GetCategoryById(CategoryId));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpGet]
        public IActionResult GetSubCategoryById(int SubCategoryId)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<SubCategoryByIdModel>(uService.GetSubCategoryById(SubCategoryId));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpGet]
        public IActionResult GetTypeById(int TypeId)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<TypeByIdModel>(uService.GetTypeById(TypeId));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpPost]
        public IActionResult UpdateCategory(CategoryrequestUpdate request)
        {
            ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
            try
            {
                if (request.CategoryName.Trim() != "")
                {

                    response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.UpdateCategory(request));

                }
                else
                {
                    response.Response.result = "PLease Enter Name";
                }

            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
            return Ok(response);

        }

        [HttpPost]
        public IActionResult UpdateSubCategory(SubCategoryrequestUpdate request)
        {
            ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
            try
            {
                if (request.SubCategoryName.Trim() != "")
                {

                    response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.UpdateSubCategory(request));

                }
                else
                {
                    response.Response.result = "PLease Enter Name";
                }

            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
            return Ok(response);

        }
        [HttpPost]
        public IActionResult UpdateType(TyperequestUpdate request)
        {
            ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
            try
            {
                if (request.TypeName.Trim() != "")
                {

                    response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.UpdateType(request));

                }
                else
                {
                    response.Response.result = "PLease Enter Name";
                }

            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
            return Ok(response);

        }
        [HttpPost]
        public IActionResult AddEditTargetGroup(TargetGroupFinal respones)
        {
            ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
            try
            {
                response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.AddEditTargetGroup(respones));

                return Ok(response);



            }
            catch (Exception ex)
            {
                return BadRequest(ex);

            }
        }
        [HttpGet]
        public IActionResult GenerateTargetCode()
        {
            ApiResponse<string> response = new ApiResponse<string>();
            try
            {
                string TGCode = "TG-" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
                response = Transform.ConvertResultToApiResonse<string>(TGCode);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpGet]
        public IActionResult GetTargetGroupList()
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<TargetGroupListModel>>(uService.GetTargetGroupList());
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpPost]
        public IActionResult GetTargetGroup(TargetGroupListRequest request)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<TargetGroupModel>>(uService.GetTargetGroup(request.TargetGroup, request.Search));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpPost]
        public IActionResult GetAddProductTG(AddProductListRequest request)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<ProductListsModel>>(uService.GetAddProductTG(request.Id));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        public IActionResult GetGeographyIdentifiers(int CurrentUserId)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<GeographyIdentifiers>>(uService.GetGeographyIdentifiers(CurrentUserId));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpPost]
        public IActionResult GetTargetById(TargetRequestModel req)

        {
            try

            {
                var response = Transform.ConvertResultToApiResonse<TargetGroupidResponse>(uService.GetTargetById(req));

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpPost]

        public IActionResult AddPackingCharges(packingshipingrequest req)
        {
            ApiResponse<ApiReturnWithResponseFlag> response = new ApiResponse<ApiReturnWithResponseFlag>();
            try
            {
                response = Transform.ConvertResultToApiResonse<ApiReturnWithResponseFlag>(uService.AddPackingCharges(req));

                return Ok(response);



            }
            catch (Exception ex)
            {
                return BadRequest(ex);

            }
        }
        [HttpPost]
        public IActionResult AddShippingCharges(packingshipingrequest req)
        {
            ApiResponse<ApiReturnWithResponseFlag> response = new ApiResponse<ApiReturnWithResponseFlag>();
            try
            {
                response = Transform.ConvertResultToApiResonse<ApiReturnWithResponseFlag>(uService.AddShippingCharges(req));

                return Ok(response);



            }
            catch (Exception ex)
            {
                return BadRequest(ex);

            }
        }
        [HttpPost]
        public IActionResult GetShippingPackingChargesList(PackingShippingRequest req)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<PackingShippingList>>(uService.GetShippingPackingChargesList(req));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpPost]
        public IActionResult GetPackingShippingChargesByID(PackingShippingByIdRequest req)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<packingshipingresponse>(uService.GetPackingShippingChargesByID(req));
      
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpGet]
        public IActionResult GetDefaultGeographydrop(int CurrentUserId)
        {
            ApiResponse<List<GeoDropDown>> response = new ApiResponse<List<GeoDropDown>>();
            try
            {
                response = Transform.ConvertResultToApiResonse<List<GeoDropDown>>(uService.GetDefaultGeographydrop(CurrentUserId));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);

            }
        }
        [HttpPost]

        public IActionResult UpdatePackingCharges(packingshipingrequest req)
        {
            ApiResponse<ApiReturnWithResponseFlag> response = new ApiResponse<ApiReturnWithResponseFlag>();
            try
            {
                response = Transform.ConvertResultToApiResonse<ApiReturnWithResponseFlag>(uService.UpdatePackingCharges(req));

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);

            }
        }
        [HttpPost]
        public IActionResult UpdateShippingCharges(packingshipingrequest req)
        {
            ApiResponse<ApiReturnWithResponseFlag> response = new ApiResponse<ApiReturnWithResponseFlag>();
            try
            {
                response = Transform.ConvertResultToApiResonse<ApiReturnWithResponseFlag>(uService.UpdateShippingCharges(req));

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}




