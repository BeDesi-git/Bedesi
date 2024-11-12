using BeDesi.Core.Models;

namespace BeDesi.Core.Services.Contracts
{
    public interface ILocationService
    {
        public Task<ApiResponse<List<string>>> GetLocationlist(string StartsWith);

        public Task<ApiResponse<List<string>>> GetAllLocations();
        public string GetPostcodeFromRegion(string region);
    }
}
