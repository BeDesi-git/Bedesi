using BeDesi.Core.Models;

namespace BeDesi.Core.Services.Contracts
{
    public interface IBusinessService
    {
        public Task<ApiResponse<List<Business>>> GetBusinesslist(SearchBusinessRequest param);
    }
}
