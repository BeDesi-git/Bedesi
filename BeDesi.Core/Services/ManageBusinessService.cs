using BeDesi.Core.Models;
using BeDesi.Core.Models.Requests;
using BeDesi.Core.Repository.Contracts;
using BeDesi.Core.Services.Contracts;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BeDesi.Core.Services
{
    public class ManageBusinessService : IManageBusinessService
    {
        private IBusinessRepository _repository;

        public ManageBusinessService(IBusinessRepository repository)
        {
            _repository = repository;
        }
        public async Task<ApiResponse<int>> AddBusiness(ManageBusinessRequest request)
        {
            var userId = GetOwnerIdFromToken(request.Token);

            var points = GetBusinessPoints(5, request.Business);

            var newBusiness = MapBusinessDetails(request);
            newBusiness.OwnerId = userId;
            newBusiness.Points = points;
            newBusiness.CreatedAt = DateTime.UtcNow;

            // Save Business to Database
            var businessAdded = await _repository.AddBusiness(newBusiness);
            return ResponseFactory.CreateResponse(businessAdded);
        }

        public async Task<ApiResponse<bool>> UpdateBusiness(ManageBusinessRequest request)
        {
            var userId = GetOwnerIdFromToken(request.Token);
            var points = GetBusinessPoints(request.Business.Points, request.Business);

            var updatedBusiness = MapBusinessDetails(request);
            updatedBusiness.BusinessId = request.Business.BusinessId;
            updatedBusiness.OwnerId = userId;
            updatedBusiness.Points = points;

            // Save Business to Database
            var businessUpdated = await _repository.UpdateBusiness(updatedBusiness);
            return ResponseFactory.CreateResponse(businessUpdated);
        }

        public async Task<ApiResponse<List<Business>>> GetBusinessByOwnerId(GetUsersBusinessRequest request)
        {
            var userId = GetOwnerIdFromToken(request.Token);
            var ownerBusinesses = await _repository.GetBusinessByOwnerId(userId);
            return ResponseFactory.CreateResponse(ownerBusinesses);
        }

        private int GetOwnerIdFromToken(string tokenFromRequest)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(tokenFromRequest);
            var userId = token.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return userId != null ? Convert.ToInt32(userId) : 0;
        }

        private int GetBusinessPoints(int points, Business business)
        {
            points = business.Website != string.Empty ? points + 3 : points;
            points = (bool)business.HasLogo ? points + 3 : points;
            points = business.InstaHandle != string.Empty ? points + 1 : points;
            points = business.Facebook != string.Empty ? points + 1 : points;
            return points;
        }

        private Business MapBusinessDetails(ManageBusinessRequest request)
        {
            var business = new Business
            {
                Name = request.Business.Name,
                Address = request.Business.Address,
                Postcode = request.Business.Postcode,
                Description = request.Business.Description,
                ContactNumber = request.Business.ContactNumber,
                Email = request.Business.Email,
                Website = request.Business.Website,
                InstaHandle = request.Business.InstaHandle,
                Facebook = request.Business.Facebook,
                HasLogo = request.Business.HasLogo,
                ServesPostcodes = request.Business.ServesPostcodes,
                Keywords = request.Business.Keywords,
                IsActive = true,
            };
            return business;
        }
    }
}
