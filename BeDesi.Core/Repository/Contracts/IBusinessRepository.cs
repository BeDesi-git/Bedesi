using BeDesi.Core.Models;

namespace BeDesi.Core.Repository.Contracts
{
    public interface IBusinessRepository
    {
        public Task<IEnumerable<Business>> GetBusinessListAsync(SearchBusinessRequest param);
    }
}
