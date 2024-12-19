using BeDesi.Core.Models;
using BeDesi.Core.Models.Requests;
using BeDesi.Core.Repository.Contracts;
using BeDesi.Core.Services.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BeDesi.Core.Services
{
    public class AuthService : IAuthService
    {
        private IUserRepository _repository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public async Task<ApiResponse<bool>> RegisterUser(RegisterRequest request)
        {
            var Salt = GenerateSalt();
            var user = new User
            {
                Email = request.Email,
                Name = request.Name,
                Salt = Salt,
                PasswordHash = HashPassword(request.Password, Salt),
                Role = "User",
                CreatedAt = DateTime.UtcNow,
                isActive = true
            };
            

            // Save User to Database
            var userRegistered = await _repository.Register(user);
            return ResponseFactory.CreateResponse(userRegistered);
        }

        public async Task<ApiResponse<AuthenticatedUser>> AuthenticateUser(LoginRequest request)
        {
            // Fetch User from Database
            var response = await _repository.GetUserByEmail(request.Email);
            if (response == null)
                return ResponseFactory.CreateFailedResponse<AuthenticatedUser>(ErrorCode.UserMessage, "User not found");
            
            // Verify password
            var isValidPassword = VerifyPassword(request.Password, response.Salt, response.PasswordHash);
            if (!isValidPassword)
                return ResponseFactory.CreateFailedResponse<AuthenticatedUser>(ErrorCode.UserMessage, "Invalid password");

            // Generate and return JWT
            var token = GenerateJwtToken(response);
            var authenticatedUser =  new AuthenticatedUser
            {
                Token = token,
                UserDetails = response
            };
            return ResponseFactory.CreateResponse(authenticatedUser);
        }

        public async Task<ApiResponse<string>> GetForgotPasswordToken(ForgotPasswordRequest request)
        {
            var user = await _repository.GetUserByEmail(request.Email);
            if(user == null)
            {
                return ResponseFactory.CreateFailedResponse<string>(ErrorCode.UserMessage, "User not found");
            }
            var token = GenerateResetToken(user);
            //Temp Reset Password Testing 
            //var resetrequest = new ResetPasswordRequest
            //{
            //    Token = token,
            //    NewPassword = "Deep@1982"
            //};
            //await ResetPassword(resetrequest);

            return ResponseFactory.CreateResponse(token);
        }

        public async Task<ApiResponse<bool>> ResetPassword(ResetPasswordRequest request)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var token = tokenHandler.ReadJwtToken(request.Token);
                var userId = token.Claims.First(c => c.Type == "nameid").Value;

                var user = await _repository.GetUserById(int.Parse(userId));
                if (user == null)
                {
                    return ResponseFactory.CreateFailedResponse<bool>(ErrorCode.UserMessage, "User not found");
                }

                // Validate the token expiration
                if (token.ValidTo < DateTime.UtcNow)
                {
                    return ResponseFactory.CreateFailedResponse<bool>(ErrorCode.UserMessage, "Reset Link Expired");
                }

                // Update the user's password
                string Salt = GenerateSalt();
                string PasswordHash = HashPassword(request.NewPassword, Salt);
                await _repository.UpdatePassword(user.UserId, Salt, PasswordHash);

                return ResponseFactory.CreateResponse(true);
            }
            catch (Exception)
            {
                return ResponseFactory.CreateFailedResponse<bool>(ErrorCode.UserMessage, "Invalid or expired token");
            }
        }
        public async Task<ApiResponse<User>> GetUserProfile(GetProfileRequest request)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var readToken = tokenHandler.ReadJwtToken(request.Token);
            var email = readToken.Claims.First(c => c.Type == "sub").Value;

            var response = await _repository.GetUserByEmail(email);
            return ResponseFactory.CreateResponse(response);

        }

        public async Task<ApiResponse<bool>> UpdateUserProfile(UpdateProfileRequest request)
        {

            var user = new User
            {
                Email = request.Email,
                Name = request.Name,
                Role = "User",
                ContactNumber = request.ContactNumber,
                CreatedAt = DateTime.UtcNow,
                isActive = true
            };

            if (request.Password != null && request.Password != string.Empty)
            {
                var Salt = GenerateSalt();
                var PasswordHash = HashPassword(request.Password, Salt);
                user.Salt = Salt;
                user.PasswordHash = PasswordHash;
            }


            // Save User to Database
            var userRegistered = await _repository.Update(user);
            return ResponseFactory.CreateResponse(userRegistered);
        }

        private string GenerateSalt()
        {
            var rng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[16];
            rng.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }
        private string HashPassword(string password, string salt)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(salt));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hash);
        }
        private bool VerifyPassword(string enteredPassword, string storedSalt, string storedHash)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(storedSalt));
            var computedHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(enteredPassword)));
            return computedHash == storedHash;
        }
        public string GenerateJwtToken(User user)
        {
            var secretKey = _configuration["JwtSettings:SecretKey"]; 

            if (string.IsNullOrEmpty(secretKey) || secretKey.Length < 16)
                throw new InvalidOperationException("The signing key is missing or too short.");

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim("username", user.Name),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private string GenerateResetToken(User user)
        {
            // Example: Generate a token that expires in 1 hour
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
