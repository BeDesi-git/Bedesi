using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;
using BeDesi.Core.Models;
using BeDesi.Core.Repository.Contracts;
using Microsoft.Extensions.Configuration;

namespace BeDesi.Core.Repository
{
    public class LocationRepository : BaseRepository, ILocationRepository
    {
        private readonly string _connectionString;
        private List<string> _locations;
        private List<Location> _locationDetails;

        public LocationRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:BeDesiDB"];
            _locations = new List<string>();
            _locationDetails = new List<Location>();
        }

        private async Task LoadLocationsAsync()
        {
            if (_locations.Count == 0 && _locationDetails.Count == 0)
            {
                _locationDetails.Clear();
                _locations.Clear();

                string query = "SELECT location_id, postcode, region FROM bds_location";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var location = new Location
                                {
                                    LocationId = reader.GetInt32(0),
                                    Postcode = reader.GetString(1),
                                    Region = ReadDbNullStringSafely(reader, 2)
                                };
                                _locationDetails.Add(location);
                                if (!_locations.Contains(location.Postcode))
                                {
                                    _locations.Add(location.Postcode);
                                }
                                if (!_locations.Contains(location.Region))
                                {
                                    _locations.Add(location.Region);
                                }
                            }
                        }
                    }
                }
            }
        }

        public async Task<IEnumerable<string>> GetOutcodeListAsync(string startsWith)
        {
            await LoadLocationsAsync();
            return _locationDetails.Where(l => l.Postcode.StartsWith(startsWith)).Select(l => l.Postcode);
        }

        public async Task<IEnumerable<string>> GetLocationListAsync(string startsWith)
        {
            await LoadLocationsAsync();
            return _locations.Where(p => p.ToLower().StartsWith(startsWith.ToLower()));
        }

        public string GetPostcodeFromRegion(string region)
        {
            
            var postCode = region;
            if (!string.IsNullOrWhiteSpace(postCode))
            {
                if (_locationDetails.Count != 0)
                {
                    var location = _locationDetails.FirstOrDefault(l => l.Region.ToLower() == region.ToLower());
                    if (location != null)
                    {
                        postCode = location.Postcode;
                    }
                }
            }
            return postCode;
        }
    }
}
