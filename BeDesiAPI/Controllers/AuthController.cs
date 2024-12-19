using BeDesi.Core.Helpers;
using BeDesi.Core.Models;
using BeDesi.Core.Models.Requests;
using BeDesi.Core.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BeDesiAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private static readonly BeDesi.Core.Helpers.ILogger log = LogProvider.GetLogger("AuthController");
        private readonly IAuthService _service;
        private readonly IEmailService _emailService;

        public AuthController(IAuthService authService, IEmailService emailService)
        {
            _service = authService;
            _emailService = emailService;
        }

        [HttpPost("register")]
        public async Task<ApiResponse<bool>> Register([FromBody] RegisterRequest param)
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
                    return ResponseFactory.CreateFailedResponse<bool>(ErrorCode.ParameterMissing, "Server is expecting {param} of type Register Request");
                }
                rid = param.RID;
                var response = await _service.RegisterUser(param);
                response.RID = rid;
                return ResponseFactory.CreateResponse(response.Result);
            }
            catch (Exception ex)
            {
                string userDisplayErrorMessage = ex.Message + " There was an issue reported. Please reach out us at 1800-5623-645. Request ID:" + "";
                log.Error(param.Email + ":" + ex.Message, ex);
                return ResponseFactory.CreateFailedResponse<bool>(ErrorCode.UnhandledError, userDisplayErrorMessage);
            }

        }

        [HttpPost("login")]
        public async Task<ApiResponse<AuthenticatedUser>> Login([FromBody] LoginRequest param)
        {
            try
            {
                string rid = "[NO-RID]";
                if (param == null || string.IsNullOrWhiteSpace(param.RID))
                {
                    return ResponseFactory.CreateFailedResponse<AuthenticatedUser>(ErrorCode.ParameterMissing, "Server is expecting RID in every request");
                }
                if (param == null)
                {
                    return ResponseFactory.CreateFailedResponse<AuthenticatedUser>(ErrorCode.ParameterMissing, "Server is expecting {param} of type LocationRequest");
                }
                rid = param.RID;
                var response = await _service.AuthenticateUser(param);

                if (response.Result == null)
                    return ResponseFactory.CreateFailedResponse<AuthenticatedUser>(ErrorCode.UnhandledError, "Unauthorized user");
                response.RID = rid;

                return ResponseFactory.CreateResponse(response.Result);
            }
            catch (Exception ex)
            {
                string userDisplayErrorMessage = ex.Message + " There was an issue reported. Please reach out us at 1800-5623-645. Request ID:" + "";
                log.Error(param.Email + ":" + ex.Message, ex);
                return ResponseFactory.CreateFailedResponse<AuthenticatedUser>(ErrorCode.UnhandledError, userDisplayErrorMessage);
            }
        }

        [HttpPost("forgot-password")]
        public async Task<ApiResponse<bool>> ForgotPassword([FromBody] ForgotPasswordRequest param)
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
                    return ResponseFactory.CreateFailedResponse<bool>(ErrorCode.ParameterMissing, "Server is expecting {param} of type LocationRequest");
                }
                rid = param.RID;

                var response = await _service.GetForgotPasswordToken(param);
                if (response.Result == null)
                    return ResponseFactory.CreateFailedResponse<bool>(ErrorCode.UnhandledError, "User not found");

                var token = response.Result;
                var resetLink = $"{Request.Scheme}://{Request.Host}/reset-password?token={token}";

                var emailBody = $"<p>Click <a href='{resetLink}'>here</a> to reset your Bedesi.co.uk password.</p>";

                await _emailService.SendEmailAsync(param.Email, "Bedesi Reset Password Request", emailBody);
                
                var result = ResponseFactory.CreateResponse(true);
                result.RID = rid;
                return result;
            }
            catch (Exception ex)
            {
                string userDisplayErrorMessage = ex.Message + " There was an issue reported. Please reach out us at 1800-5623-645. Request ID:" + "";
                log.Error(param.Email + ":" + ex.Message, ex);
                return ResponseFactory.CreateFailedResponse<bool>(ErrorCode.UnhandledError, userDisplayErrorMessage);
            }
        }

        [HttpPost("reset-password")]
        public async Task<ApiResponse<bool>> ResetPassword([FromBody] ResetPasswordRequest param)
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
                    return ResponseFactory.CreateFailedResponse<bool>(ErrorCode.ParameterMissing, "Server is expecting {param} of type LocationRequest");
                }
                rid = param.RID;
                var response = await _service.ResetPassword(param);
                if (response.Result)
                    return ResponseFactory.CreateFailedResponse<bool>(ErrorCode.UnhandledError, "Could not reset the password: " + response.ErrorMessage);
                response.RID = rid;
                return response;
            }
            catch (Exception ex)
            {
                string userDisplayErrorMessage = ex.Message + " There was an issue reported. Please reach out us at 1800-5623-645. Request ID:" + "";
                log.Error(param.RID + ":" + ex.Message, ex);
                return ResponseFactory.CreateFailedResponse<bool>(ErrorCode.UnhandledError, userDisplayErrorMessage);
            }
        }
    }
}


