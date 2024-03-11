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
    public class UserMgmtApi : ControllerBase
    {
        private readonly IUserMgmtService uService;
        public UserMgmtApi(IUserMgmtService _uService)
        {
            uService = _uService;
        }
        [HttpPost]
        public IActionResult GetAllUsers(userListRequest request)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<UsersListGrid>>(uService.GetUsersList(request.userTypes, request.statuss, request.Search));
                response.totalRecords = response.Response.Count;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        public IActionResult GetUserStatusList()
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<UserStatusDD>>(uService.GetUserStatusList());
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        public IActionResult GetUserTypes()
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<RolesDropDown>>(uService.GetAllUserTypes());
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpPost]
        public IActionResult AddEditUser(AddEditUserRequestModel request)
//            string FirstName, string LastName, string Email, string UserName, string MobilePhone, int RoleId, int Userid = 0)
        {
            try
            {
                ApiResponse<SPReturnString> response = new ApiResponse<SPReturnString>();
                if (request.UserId > 0)
                {
                    response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.UpdateUser(request.FirstName, request.LastName, request.Email, request.UserName, request.MobilePhone, request.RoleId, request.UserId));
                }
                else
                {
                    response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.AddUser(request.FirstName, request.LastName, request.Email, request.UserName, request.MobilePhone, request.RoleId, request.UserId));
                }

                return Ok(response);
            }
            catch (Exception ex)           {
                return Ok(ex);
            }
        }
        [HttpPost]
        public IActionResult UpdatePassword(UpdatePasswordModel request)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<string>(uService.UpdatePassword(request.UserId,request.currentPassword,request.newPassword));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        public IActionResult UpdatePasswordByAdmin(UpdatePasswordAdminModel request)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<string>(uService.UpdatePasswordByAdmin(request.UserId, request.CurrentUserId, request.newPassword));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }


        public IActionResult GetUserDetailsToEdit(int userId)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<AddEditUserModel>(uService.GetUserDetailsToEdit(userId));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        public IActionResult AddEditStatus(string StatusName, int DoneBy, int entityId, bool isActive, int statusId, string displayName = "")
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.AddEditStatus(StatusName, DoneBy, entityId, isActive, statusId, displayName));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        public IActionResult AddEditUserRole(int roleId, string roleName, int DoneBy, bool isActive)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.AddEditUserRole(roleId, roleName, DoneBy, isActive));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        public IActionResult ActiveDeactive(ActivateDeactivateModel request)
        {
            try
            {


                var response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.ActiveDeactive(request.UserId,request.logedUserId, request.status));
                return Ok(response);

            }
            catch (Exception ex)
            {
                return Ok(ex);
            }

            

            
        }
        public IActionResult UpdateProfile(EditProfile request)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<SPReturnString>(uService.UpdateProfile(request));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        public IActionResult GetUserRoles(UserRoleAccessRequest request)
        {
            try
            {
                var response = Transform.ConvertResultToApiResonse<List<UserRoleAccessModel>>(uService.GetUserRoles(request));
                return Ok(response);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

