using BeDesi.Core.Models;

namespace BeDesi.Core.Services.Contracts
{
    public interface IAuthService
    {
        Task<ApiResponse<bool>> RegisterUser(RegisterRequest request);
        Task<ApiResponse<AuthenticatedUser>> AuthenticateUser(LoginRequest request);
        Task<ApiResponse<string>> GetForgotPasswordToken(ForgotPasswordRequest request);
        Task<ApiResponse<bool>> ResetPassword(ResetPasswordRequest request);
        Task<ApiResponse<User>> GetUserProfile(GetProfileRequest request);
        Task<ApiResponse<bool>> UpdateUserProfile(UpdateProfileRequest request);

    }
}
