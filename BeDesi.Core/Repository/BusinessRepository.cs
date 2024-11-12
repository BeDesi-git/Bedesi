using System.Data.SqlClient;
using BeDesi.Core.Models;
using BeDesi.Core.Repository.Contracts;


namespace BeDesi.Core.Repository
{
    public class BusinessRepository : BaseRepository, IBusinessRepository
    {
        private readonly string _connectionString;
        public BusinessRepository(string connectionString)
        {
            _connectionString = connectionString;   
        }
        public async Task<IEnumerable<Business>> GetBusinessListAsync(SearchBusinessRequest param)
        {
            var businesses = new List<Business>();

            // SQL query with parameters
            string query = "SELECT business_id, name, address, postcode, description, contact_number, website, insta_handle, facebook, has_logo " +
                           "FROM bds_business " +
                           "WHERE keywords LIKE @keywords AND serves_postcode LIKE @postcode";

            // Using SqlConnection to connect to the database
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // Open the connection
                await connection.OpenAsync();

                // Create a SqlCommand with the query and the connection
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters with wildcards in C#
                    command.Parameters.AddWithValue("@keywords", "%" + param.Keywords + "%");
                    command.Parameters.AddWithValue("@postcode", "%" + param.Location + "%");

                    // Execute the query and retrieve the results using SqlDataReader
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        // Read the results asynchronously
                        while (await reader.ReadAsync())
                        {
                            var business = new Business
                            {
                                BusinessId = reader.GetInt32(0),    // Assuming the first column is business_id
                                Name = reader.GetString(1),         // Assuming name is the second column
                                Address = ReadDbNullStringSafely(reader, 2),      // Added Address property
                                Postcode = ReadDbNullStringSafely(reader, 3),
                                Description = ReadDbNullStringSafely(reader, 4),  // Assuming description is third
                                ContactNumber = ReadDbNullStringSafely(reader, 5),
                                Website = ReadDbNullStringSafely(reader, 6),
                                InstaHandle = ReadDbNullStringSafely(reader, 7),
                                Facebook = ReadDbNullStringSafely(reader, 8),
                                HasLogo = ReadDbNullStringSafely(reader, 9)
                            };

                            businesses.Add(business); // Add to the list
                        }
                    }
                }
            }

            return businesses;  // Return the list of businesses
        }
    }
}
