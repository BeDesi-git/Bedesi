using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BeDesi.Core.Services.Contracts;
using BeDesi.Core.Helpers;
using BeDesi.Core.Models;
using BeDesi.Core.Models.Requests;

[ApiController]
[Route("api/[controller]")]
public class ProfileController : ControllerBase
{
    private static readonly BeDesi.Core.Helpers.ILogger log = LogProvider.GetLogger("UserProfileController");
    private readonly IAuthService _service;

    public ProfileController(IAuthService service)
    {
        _service = service;
    }

    
    [HttpGet("[action]")]
    public async Task<ApiResponse<User>> GetUserProfile([FromQuery] GetProfileRequest param)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get user ID from token
            var response = await _service.GetUserProfile(param);
            if (response.Result == null)
            {
                return ResponseFactory.CreateFailedResponse<User>(ErrorCode.UnhandledError, "User Profile not found");
            }
            return ResponseFactory.CreateResponse(response.Result);
        }
        catch (Exception ex)
        {
            string userDisplayErrorMessage = ex.Message + " There was an issue reported. Please reach out us at 1800-5623-645. Request ID:" + "";
            log.Error(ex.Message, ex);
            return ResponseFactory.CreateFailedResponse<User>(ErrorCode.UnhandledError, userDisplayErrorMessage);
        }
    }

   [HttpPut("[action]")]
    public async Task<ApiResponse<bool>> UpdateProfile([FromBody] UpdateProfileRequest request)
    {
        var response = await _service.UpdateUserProfile(request);
        if (!response.Result)
        {
            return ResponseFactory.CreateFailedResponse<bool>(ErrorCode.UnhandledError, "Failed to update profile.");
        }

        return ResponseFactory.CreateResponse(true);
    }
}
