using BeDesi.Core.Models;
using Microsoft.AspNetCore.Mvc;
using BeDesi.Core.Helpers;
using BeDesi.Core.Services.Contracts;
using BeDesi.Core.Models.Requests;

namespace BeDesiAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : ControllerBase
    {
        private static readonly BeDesi.Core.Helpers.ILogger log = LogProvider.GetLogger("LocationController");
        private ILocationService _service;
        public LocationController(ILocationService locationService)
        {
            _service = locationService;
        }

        [HttpGet("[action]")]
        public async Task<ApiResponse<List<string>>> GetLocation([FromQuery] LocationRequest param)
        {
            try
            {
                string rid = "[NO-RID]";
                if (param == null || string.IsNullOrWhiteSpace(param.RID))
                {
                    return ResponseFactory.CreateFailedResponse<List<string>>(ErrorCode.ParameterMissing, "Server is expecting RID in every request");
                }
                if (param == null)
                {
                    return ResponseFactory.CreateFailedResponse<List<string>>(ErrorCode.ParameterMissing, "Server is expecting {param} of type LocationRequest");
                }
                rid = param.RID;
                var result = await _service.GetLocationlist(param.StartsWith);
                result.RID = rid;
                return result;
            }
            catch (System.Exception ex)
            {
                string errorTemp = "[ERROR] " + Newtonsoft.Json.JsonConvert.SerializeObject(ex) + " [/ERROR] ";
                string userDisplayErrorMessage = "There was an issue reported. Please reach out us at +91 99999999. Request ID:" + param.RID;
                log.Error("Error in API RequestID : " + param.RID, ex);
                return ResponseFactory.CreateFailedResponse<List<string>>(ErrorCode.UnhandledError, userDisplayErrorMessage, errorTemp);
            }
        }

        [HttpGet("[action]")]
        public async Task<ApiResponse<List<string>>> GetAllLocations([FromQuery] AllLocationRequest param)
        {
            try
            {
                string rid = "[NO-RID]";
                if (param == null || string.IsNullOrWhiteSpace(param.RID))
                {
                    return ResponseFactory.CreateFailedResponse<List<string>>(ErrorCode.ParameterMissing, "Server is expecting RID in every request");
                }
                if (param == null)
                {
                    return ResponseFactory.CreateFailedResponse<List<string>>(ErrorCode.ParameterMissing, "Server is expecting {param} of type LocationRequest");
                }
                rid = param.RID;
                var result = await _service.GetAllLocations();
                result.RID = rid;
                return result;
            }
            catch (System.Exception ex)
            {
                string errorTemp = "[ERROR] " + Newtonsoft.Json.JsonConvert.SerializeObject(ex) + " [/ERROR] ";
                string userDisplayErrorMessage = "There was an issue reported. Please reach out us at +91 99999999. Request ID:" + param.RID;
                log.Error("Error in API RequestID : " + param.RID, ex);
                return ResponseFactory.CreateFailedResponse<List<string>>(ErrorCode.UnhandledError, userDisplayErrorMessage, errorTemp);
            }
        }

        [HttpGet("[action]")]
        public async Task<ApiResponse<List<string>>> GetAllOutcodes([FromQuery] AllLocationRequest param)
        {
            try
            {
                string rid = "[NO-RID]";
                if (param == null || string.IsNullOrWhiteSpace(param.RID))
                {
                    return ResponseFactory.CreateFailedResponse<List<string>>(ErrorCode.ParameterMissing, "Server is expecting RID in every request");
                }
                if (param == null)
                {
                    return ResponseFactory.CreateFailedResponse<List<string>>(ErrorCode.ParameterMissing, "Server is expecting {param} of type LocationRequest");
                }
                rid = param.RID;
                var result = await _service.GetAllOutcodes();
                result.RID = rid;
                return result;
            }
            catch (System.Exception ex)
            {
                string errorTemp = "[ERROR] " + Newtonsoft.Json.JsonConvert.SerializeObject(ex) + " [/ERROR] ";
                string userDisplayErrorMessage = "There was an issue reported. Please reach out us at +91 99999999. Request ID:" + param.RID;
                log.Error("Error in API RequestID : " + param.RID, ex);
                return ResponseFactory.CreateFailedResponse<List<string>>(ErrorCode.UnhandledError, userDisplayErrorMessage, errorTemp);
            }
        }
    }
}
