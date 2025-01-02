using BeDesi.Core.Helpers;
using BeDesi.Core.Models;
using BeDesi.Core.Models.Requests;
using BeDesi.Core.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BeDesiAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManageBusinessController
    {
        private static readonly BeDesi.Core.Helpers.ILogger log = LogProvider.GetLogger("ManageBusinessController");
        private readonly IManageBusinessService _service;

        public ManageBusinessController(IManageBusinessService manageBusinessService)
        {
            _service = manageBusinessService;
        }

        [HttpPost("[action]")]
        public async Task<ApiResponse<int>> AddBusiness([FromBody] ManageBusinessRequest param)
        {
            try
            {
                string rid = "[NO-RID]";
                if (param == null || string.IsNullOrWhiteSpace(param.RID))
                {
                    return ResponseFactory.CreateFailedResponse<int>(ErrorCode.ParameterMissing, "Server is expecting RID in every request");
                }
                if (param == null)
                {
                    return ResponseFactory.CreateFailedResponse<int>(ErrorCode.ParameterMissing, "Server is expecting {param} of type Add Buisness");
                }
                rid = param.RID;
                var response = await _service.AddBusiness(param);
                response.RID = rid;
                return ResponseFactory.CreateResponse(response.Result);
            }
            catch (Exception ex)
            {
                string userDisplayErrorMessage = ex.Message + " There was an issue reported. Please reach out us at 1800-5623-645. Request ID:" + "";
                log.Error(param.Token + ":" + ex.Message, ex);
                return ResponseFactory.CreateFailedResponse<int>(ErrorCode.UnhandledError, userDisplayErrorMessage);
            }

        }

        [HttpPost("[action]")]
        public async Task<ApiResponse<bool>> UpdateBusiness([FromBody] ManageBusinessRequest param)
        {
            try
            {
                string rid = "[NO-RID]";
                if (param == null || string.IsNullOrWhiteSpace(param.RID))
                {
                    return ResponseFactory.CreateFailedResponse<bool>(ErrorCode.ParameterMissing, "Server is expecting RID in every request");
                }
                if (param == null)
                {
                    return ResponseFactory.CreateFailedResponse<bool>(ErrorCode.ParameterMissing, "Server is expecting {param} of type Update Buisness");
                }
                rid = param.RID;
                var response = await _service.UpdateBusiness(param);
                response.RID = rid;
                return ResponseFactory.CreateResponse(response.Result);
            }
            catch (Exception ex)
            {
                string userDisplayErrorMessage = ex.Message + " There was an issue reported. Please reach out us at 1800-5623-645. Request ID:" + "";
                log.Error(param.Token + ":" + ex.Message, ex);
                return ResponseFactory.CreateFailedResponse<bool>(ErrorCode.UnhandledError, userDisplayErrorMessage);
            }

        }
        [HttpGet("[action]")]
        public async Task<ApiResponse<List<Business>>> GetUserBusiness([FromQuery] GetUsersBusinessRequest param)
        {
            try
            {
                string rid = "[NO-RID]";
                if (param == null || string.IsNullOrWhiteSpace(param.RID))
                {
                    return ResponseFactory.CreateFailedResponse<List<Business>>(ErrorCode.ParameterMissing, "Server is expecting RID in every request");
                }
                if (param == null)
                {
                    return ResponseFactory.CreateFailedResponse<List<Business>>(ErrorCode.ParameterMissing, "Server is expecting {param} of type Get User Buisness");
                }
                rid = param.RID;
                var response = await _service.GetBusinessByOwnerId(param);
                response.RID = rid;
                return ResponseFactory.CreateResponse(response.Result);
            }
            catch (Exception ex)
            {
                string userDisplayErrorMessage = ex.Message + " There was an issue reported. Please reach out us at 1800-5623-645. Request ID:" + "";
                log.Error(param.Token + ":" + ex.Message, ex);
                return ResponseFactory.CreateFailedResponse<List<Business>>(ErrorCode.UnhandledError, userDisplayErrorMessage);
            }

        }

        [HttpGet("[action]")]
        public async Task<ApiResponse<bool>> CheckBusinessName([FromQuery] CheckBusinessNameRequest param)
        {
            try
            {
                string rid = "[NO-RID]";
                if (param == null || string.IsNullOrWhiteSpace(param.RID))
                {
                    return ResponseFactory.CreateFailedResponse<bool>(ErrorCode.ParameterMissing, "Server is expecting RID in every request");
                }
                if (param == null)
                {
                    return ResponseFactory.CreateFailedResponse<bool>(ErrorCode.ParameterMissing, "Server is expecting {param} of type Check Business Name");
                }
                rid = param.RID;
                var response = await _service.CheckBusinessName(param);
                response.RID = rid;
                return ResponseFactory.CreateResponse(response.Result);
            }
            catch (Exception ex)
            {
                string userDisplayErrorMessage = ex.Message + " There was an issue reported. Please reach out us at 1800-5623-645. Request ID:" + "";
                log.Error(param.BusinessName + ":" + ex.Message, ex);
                return ResponseFactory.CreateFailedResponse<bool>(ErrorCode.UnhandledError, userDisplayErrorMessage);
            }
        }
    }
}


