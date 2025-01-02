using System.Configuration;
using System.Data.SqlClient;
using BeDesi.Core.Models;
using BeDesi.Core.Repository.Contracts;
using Microsoft.Extensions.Configuration;

namespace BeDesi.Core.Repository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:BeDesiDB"];
        }

        public async Task<bool> Register(User user)
        {
            // SQL query for inserting data into the bds_business table
            string query = @"
                            INSERT INTO bds_user
                            (email, name, password_hash, salt, role, is_active) 
                            VALUES 
                            (@email, @name, @passwordHash, @salt, @role, @isActive)";


            // Using SqlConnection to connect to the database
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // Open the connection
                await connection.OpenAsync();

                // Create a SqlCommand with the query and the connection
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters with their values
                    command.Parameters.AddWithValue("@email", user.Email ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@name", user.Name ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@passwordHash", user.PasswordHash ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@salt", user.Salt ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@role", user.Role ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@isActive", user.isActive);
                    
                    // Execute the query and check rows affected
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    // Return true if at least one row was inserted
                    return rowsAffected > 0;
                }
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {
            // SQL query to fetch user details by email
            string query = @"
                            SELECT user_id, name, email, password_hash, salt, role, contact_number
                            FROM bds_user
                            WHERE email = @Email";

            // Using SqlConnection to connect to the database
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // Open the connection
                await connection.OpenAsync();

                // Create a SqlCommand with the query and the connection
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameter for email
                    command.Parameters.AddWithValue("@Email", email);

                    // Execute the query and retrieve the results using SqlDataReader
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        // Check if a record is found
                        if (await reader.ReadAsync())
                        {
                            // Map the data to a User object
                            var user = new User
                            {
                                UserId = reader.GetInt32(0),         
                                Name = reader.GetString(1),          
                                Email = reader.GetString(2),         
                                PasswordHash = reader.GetString(3),  
                                Salt = reader.GetString(4),          
                                Role = reader.GetString(5) ,
                                ContactNumber = ReadDbNullStringSafely(reader, 6)
                            };

                            return user; // Return the User object
                        }
                    }
                }
            }

            return null; // Return null if no user was found
        }

        public async Task<User> GetUserById(int id)
        {
            // SQL query to fetch user details by email
            string query = @"
                            SELECT user_id, name, email, password_hash, salt, role 
                            FROM bds_user
                            WHERE user_id = @Id";

            // Using SqlConnection to connect to the database
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // Open the connection
                await connection.OpenAsync();

                // Create a SqlCommand with the query and the connection
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameter for email
                    command.Parameters.AddWithValue("@Id", id);

                    // Execute the query and retrieve the results using SqlDataReader
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        // Check if a record is found
                        if (await reader.ReadAsync())
                        {
                            // Map the data to a User object
                            var user = new User
                            {
                                UserId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Email = reader.GetString(2),
                                PasswordHash = reader.GetString(3),
                                Salt = reader.GetString(4),
                                Role = reader.GetString(5)
                            };

                            return user; // Return the User object
                        }
                    }
                }
            }

            return null; // Return null if no user was found
        }

        public async Task<bool> UpdatePassword(int id, string salt, string passwordHash)
        {
            // SQL query to update the password in the bds_user table
            string query = @"
                    UPDATE bds_user
                    SET password_hash = @passwordHash, salt = @salt
                    WHERE user_id = @id";

            // Using SqlConnection to connect to the database
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // Open the connection
                await connection.OpenAsync();

                // Create a SqlCommand with the query and the connection
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters with their values
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@passwordHash", passwordHash);
                    command.Parameters.AddWithValue("@salt", salt);

                    // Execute the query and check rows affected
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    // Return true if at least one row was updated
                    return rowsAffected > 0;
                }
            }
        }

        public async Task<bool> Update(User user)
        {
            // SQL query for updating user details
            string query = @"
                    UPDATE bds_user
                    SET 
                        name = @name,
                        contact_number = @contactNumber,
                        password_hash = CASE WHEN @passwordHash IS NOT NULL THEN @passwordHash ELSE password_hash END,
                        salt = CASE WHEN @salt IS NOT NULL THEN @salt ELSE salt END,
                        role = @role
                    WHERE 
                        email = @email";

            // Using SqlConnection to connect to the database
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // Open the connection
                await connection.OpenAsync();

                // Create a SqlCommand with the query and the connection
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters with their values
                    command.Parameters.AddWithValue("@name", user.Name ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@contactNumber", user.ContactNumber ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@passwordHash", user.PasswordHash ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@salt", user.Salt ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@role", user.Role ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@email", user.Email ?? (object)DBNull.Value);

                    // Execute the query and check rows affected
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    // Return true if at least one row was updated
                    return rowsAffected > 0;
                }
            }
        }


    }
}
