using BeDesi.Core.Models;
using Microsoft.AspNetCore.Mvc;
using BeDesi.Core.Helpers;
using BeDesi.Core.Services.Contracts;

namespace BeDesiAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusinessController : ControllerBase
    {
        private static readonly BeDesi.Core.Helpers.ILogger log = LogProvider.GetLogger("BusinessListService");
        private IBusinessService _service;
        public BusinessController(IBusinessService businessService )
        {
            _service = businessService;
        }

        [HttpGet]
        public async Task<ApiResponse<List<Business>>> SearchBusinesses([FromQuery] SearchBusinessRequest param)
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
                    return ResponseFactory.CreateFailedResponse<List<Business>>(ErrorCode.ParameterMissing, "Server is expecting {param} of type LocationRequest");
                }
                rid = param.RID;
                var result = await _service.GetBusinesslist(param);
                result.RID = rid;
                return result;
            }
            catch (System.Exception ex)
            {
                string errorTemp = "[ERROR] " + Newtonsoft.Json.JsonConvert.SerializeObject(ex) + " [/ERROR] ";
                string userDisplayErrorMessage = "There was an issue reported. Please reach out us at +91 99999999. Request ID:" + param.RID;
                log.Error("Error in API RequestID : " + param.RID, ex);
                return ResponseFactory.CreateFailedResponse<List<Business>>(ErrorCode.UnhandledError, userDisplayErrorMessage, errorTemp);
            }
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Business>> GetBusiness(int id)
        //{
        //    var business = await _context.Businesses.FindAsync(id);
        //    if (business == null)
        //    {
        //        return NotFound();
        //    }
        //    return business;
        //}

        //[HttpPost("{id}/rate")]
        //public async Task<IActionResult> RateBusiness(int id, [FromBody] BusinessRating rating)
        //{
        //    var business = await _context.Businesses.FindAsync(id);
        //    if (business == null)
        //    {
        //        return NotFound();
        //    }
        //    rating.BusinessId = id;
        //    _context.BusinessRatings.Add(rating);
        //    await _context.SaveChangesAsync();
        //    return Ok();
        //}
    }
}
