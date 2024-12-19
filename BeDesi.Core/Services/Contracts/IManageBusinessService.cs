using BeDesi.Core.Models;
using BeDesi.Core.Models.Requests;

namespace BeDesi.Core.Services.Contracts
{
    public interface IManageBusinessService
    {
        Task<ApiResponse<int>> AddBusiness(ManageBusinessRequest request);
        Task<ApiResponse<bool>> UpdateBusiness(ManageBusinessRequest request);
        Task<ApiResponse<List<Business>>> GetBusinessByOwnerId(GetUsersBusinessRequest request);
    }
}
