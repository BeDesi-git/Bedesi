using BeDesi.Core.Models;
using BeDesi.Core.Models.Requests;

namespace BeDesi.Core.Repository.Contracts
{
    public interface IBusinessRepository
    {
        public Task<IEnumerable<Business>> GetBusinessListAsync(SearchBusinessRequest param);
        public Task<int> AddBusiness(Business newBusiness);
        public Task<bool> UpdateBusiness(Business updatedBusiness);
        public Task<List<Business>> GetBusinessByOwnerId(int ownerId);
        public Task<bool> CheckBusinessName(string businessName);
    }
}
