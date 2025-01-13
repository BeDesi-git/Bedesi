using BeDesi.Core.Models;
using BeDesi.Core.Models.Requests;

namespace BeDesi.Core.Services.Contracts
{
    public interface IAuthService
    {
        Task<ApiResponse<int>> RegisterUser(RegisterRequest request);
        Task<ApiResponse<AuthenticatedUser>> AuthenticateUser(LoginRequest request);
        Task<ApiResponse<string>> GetForgotPasswordToken(ForgotPasswordRequest request);
        Task<ApiResponse<bool>> ResetPassword(ResetPasswordRequest request);
        Task<ApiResponse<User>> GetUserProfile(GetProfileRequest request);
        Task<ApiResponse<bool>> UpdateUserProfile(UpdateProfileRequest request);
    }
}
