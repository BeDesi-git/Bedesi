using BeDesi.Core.Models;
using BeDesi.Core.Repository.Contracts;
using BeDesi.Core.Services.Contracts;

namespace BeDesi.Core.Services
{
    public class BusinessService : IBusinessService
    {
        private IBusinessRepository _repository;
        private ILocationService _locationService;

        public BusinessService(IBusinessRepository repository, ILocationService locationService)
        {
            _repository = repository;
            _locationService = locationService; 
        }

        public async Task<ApiResponse<List<Business>>> GetBusinesslist(SearchBusinessRequest param)
        {
            List<Business> response = null;

            param.Location = _locationService.GetPostcodeFromRegion(param.Location);

            //Get from Databsse
            var result = await _repository.GetBusinessListAsync(param);
            response = result.ToList();

            if (response == null)
            {
                ResponseFactory.CreateFailedResponse<Business>(ErrorCode.UserMessage, "No Business found");
            }

            return ResponseFactory.CreateResponse(response);
        }
    }
}
