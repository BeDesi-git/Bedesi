using BeDesi.Core.Models;
using BeDesi.Core.Repository.Contracts;
using BeDesi.Core.Services.Contracts;

namespace BeDesi.Core.Services
{
    public class LocationService : ILocationService
    {
        private ILocationRepository _repository;

        public LocationService(ILocationRepository repository)
        {
            _repository = repository;
        }
        public async Task<ApiResponse<List<string>>> GetLocationlist(string StartsWith)
        {
            List<string> response = null;

            //Get from Databsse
            var result = await _repository.GetLocationListAsync(StartsWith);
            response = result.ToList();

            if (response == null)
            {
                ResponseFactory.CreateFailedResponse<string>(ErrorCode.UserMessage, "No Location found");
            }

            return ResponseFactory.CreateResponse(response);
        }

        public async Task<ApiResponse<List<string>>> GetAllLocations()
        {
            List<string> response = null;

            //Get from Databsse
            var result = await _repository.GetLocationListAsync("");
            response = result.ToList();

            if (response == null)
            {
                ResponseFactory.CreateFailedResponse<string>(ErrorCode.UserMessage, "No Location found");
            }

            return ResponseFactory.CreateResponse(response);
        }

        public string GetPostcodeFromRegion(string region)
        {
           return _repository.GetPostcodeFromRegion(region);
        }
    }
}
