using System.Data.SqlClient;
using BeDesi.Core.Models;
using BeDesi.Core.Repository.Contracts;
using Microsoft.Extensions.Configuration;


namespace BeDesi.Core.Repository
{
    public class BusinessRepository : BaseRepository, IBusinessRepository
    {
        private readonly string _connectionString;
        public BusinessRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:BeDesiDB"];
        }
        public async Task<IEnumerable<Business>> GetBusinessListAsync(SearchBusinessRequest param)
        {
            var businesses = new List<Business>();

            // Convert the keywords string into an array (comma or space-separated)
            var keywordsArray = param.Keywords?.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            string keywordConditions = string.Empty;

            if (keywordsArray != null && keywordsArray.Length > 0)
            { 
                keywordConditions = string.Join(" OR ", keywordsArray.Select((k, i) => $"keywords LIKE @keyword{i}"));
            }
            else
            {
                keywordConditions = "1=1"; 
            }

            // SQL query with parameters
            string query = $@"
            SELECT business_id, name, address, postcode, description, contact_number, email, website, insta_handle, facebook, has_logo, is_online, is_active
            FROM bds_business
            WHERE is_active = 'Y' 
              AND ({keywordConditions}) 
              AND (serves_postcode LIKE @postcode OR is_online = 'Y')";

            // Using SqlConnection to connect to the database
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // Open the connection
                await connection.OpenAsync();

                // Create a SqlCommand with the query and the connection
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters for each keyword if they exist
                    if (keywordsArray != null && keywordsArray.Length > 0)
                    {
                        for (int i = 0; i < keywordsArray.Length; i++)
                        {
                            command.Parameters.AddWithValue($"@keyword{i}", "%" + keywordsArray[i] + "%");
                        }
                    }

                    // Add the postcode parameter
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
                                Email = ReadDbNullStringSafely(reader, 6),
                                Website = ReadDbNullStringSafely(reader, 7),
                                InstaHandle = ReadDbNullStringSafely(reader, 8),
                                Facebook = ReadDbNullStringSafely(reader, 9),
                                HasLogo = ReadDbNullBoolSafely(reader, 10),
                                IsOnline = ReadDbNullBoolSafely(reader, 11)
                            };

                            businesses.Add(business); // Add to the list
                        }
                    }
                }
            }

            return businesses; // Return the list of businesses
        }

        public async Task<int> AddBusiness(Business newBusiness)
        {
            string query = @"
        INSERT INTO bds_business
            (name, address, postcode, description, contact_number, email, website,
             insta_handle, facebook, has_logo, serves_postcode, keywords, 
             points, owner_id, is_active, agree_to_show, is_online, created_at) 
        OUTPUT INSERTED.business_id
        VALUES
            (@name, @address, @postcode, @description, @contact_number, @email,
             @website, @insta_handle, @facebook, @has_logo, @serves_postcode, 
             @keywords, @points, @owner_id, @is_active, @agree_to_show, @is_online, @created_at)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", newBusiness.Name ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@address", newBusiness.Address ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@postcode", newBusiness.Postcode ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@description", newBusiness.Description ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@contact_number", newBusiness.ContactNumber ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@email", newBusiness.Email ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@website", newBusiness.Website ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@insta_handle", newBusiness.InstaHandle ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@facebook", newBusiness.Facebook ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@has_logo", newBusiness.HasLogo ? "Y" : "N");
                    command.Parameters.AddWithValue("@serves_postcode", string.Join(";", newBusiness.ServesPostcodes) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@keywords", string.Join(";", newBusiness.Keywords) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@points", newBusiness.Points);
                    command.Parameters.AddWithValue("@owner_id", newBusiness.OwnerId);
                    command.Parameters.AddWithValue("@is_active", newBusiness.IsActive ? "Y" : "N");
                    command.Parameters.AddWithValue("@agree_to_show", newBusiness.AgreeToShow ? "Y" : "N");
                    command.Parameters.AddWithValue("@is_online", newBusiness.IsOnline ? "Y" : "N");
                    command.Parameters.AddWithValue("@created_at", newBusiness.CreatedAt);

                    // Execute the query and get the returned ID
                    int businessId = (int)await command.ExecuteScalarAsync();
                    return businessId;
                }
            }
        }
        public async Task<bool> UpdateBusiness(Business updateBusiness)
        {
            string query = @"
                            UPDATE bds_business
                            SET 
                                name = @name,
                                address = @address,
                                postcode = @postcode,
                                description = @description,
                                contact_number = @contact_number,
                                email = @email,
                                website = @website,
                                insta_handle = @insta_handle,
                                facebook = @facebook,
                                has_logo = @has_logo,
                                serves_postcode = @serves_postcode,
                                keywords = @keywords,
                                points = @points,
                                is_active = @is_active,
                                is_online = @is_online
                            WHERE 
                                business_id = @business_id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", updateBusiness.Name ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@address", updateBusiness.Address ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@postcode", updateBusiness.Postcode ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@description", updateBusiness.Description ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@contact_number", updateBusiness.ContactNumber ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@email", updateBusiness.Email ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@website", updateBusiness.Website ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@insta_handle", updateBusiness.InstaHandle ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@facebook", updateBusiness.Facebook ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@has_logo", updateBusiness.HasLogo ? "Y" : "N");
                    command.Parameters.AddWithValue("@serves_postcode", string.Join(";", updateBusiness.ServesPostcodes) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@keywords", string.Join(";", updateBusiness.Keywords) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@points", updateBusiness.Points);
                    command.Parameters.AddWithValue("@is_active", updateBusiness.IsActive ? "Y" : "N");
                    command.Parameters.AddWithValue("@is_online", updateBusiness.IsOnline ? "Y" : "N");
                    command.Parameters.AddWithValue("@business_id", updateBusiness.BusinessId);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }
        public async Task<List<Business>> GetBusinessByOwnerId(int ownerId)
        {
            string query = @"
            SELECT 
                business_id, name, address, postcode, description, contact_number, email,
                website, insta_handle, facebook, has_logo, serves_postcode, keywords, 
                points, owner_id, is_active, agree_to_show, is_online, created_at
            FROM bds_business
            WHERE owner_id = @owner_id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@owner_id", ownerId);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        List<Business> businesses = new List<Business>();

                        while (await reader.ReadAsync())
                        {
                            businesses.Add(new Business
                            {
                                BusinessId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Address = reader.GetString(2),
                                Postcode = reader.GetString(3),
                                Description = reader.GetString(4),
                                ContactNumber = reader.GetString(5),
                                Email = reader.GetString(6),
                                Website = reader.GetString(7),
                                InstaHandle = reader.GetString(8),
                                Facebook = reader.GetString(9),
                                HasLogo = reader.GetString(10) == "Y" ? true : false,
                                ServesPostcodes = reader.GetString(11).Split(';').ToList(),
                                Keywords = reader.GetString(12).Split(';').ToList(),
                                Points = reader.GetInt32(13),
                                OwnerId = reader.GetInt32(14),
                                IsActive = reader.GetString(15) == "Y" ? true : false,
                                AgreeToShow = reader.GetString(16) == "Y" ? true : false,
                                IsOnline = reader.GetString(17) == "Y" ? true : false,//,
                                //CreatedAt = DateTime.Parse(reader.GetString(17))
                            });
                        }

                        return businesses;
                    }
                }
            }
        }

        public async Task<bool> CheckBusinessName(string businessName)
        {
            string query = @"
                            SELECT COUNT(1)
                            FROM bds_business
                            WHERE LOWER(name) = LOWER(@name)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", businessName ?? (object)DBNull.Value);

                    // Execute the query and return whether the count is greater than 0
                    int count = (int)await command.ExecuteScalarAsync();
                    return count > 0;
                }
            }
        }


    }
}
