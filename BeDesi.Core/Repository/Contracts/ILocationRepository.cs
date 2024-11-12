using BeDesi.Core.Models;

namespace BeDesi.Core.Repository.Contracts
{
    public interface ILocationRepository
    {
        public Task<IEnumerable<string>> GetLocationListAsync(string StartsWith);
        public string GetPostcodeFromRegion(string region);
    }
}
