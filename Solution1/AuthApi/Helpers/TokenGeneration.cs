using DataServices.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthApi.Helpers
{
    public class TokenGeneration
    {
        private IConfiguration _config;
        private ILogger<TokenGeneration> _logger;
        private string[] SourceRoles = new[] { "Admin", "User" };
        private IEmployeeLoginRepository _employeeLoginRepository;
        public TokenGeneration(IConfiguration config, ILogger<TokenGeneration> logger, IEmployeeLoginRepository employeeLoginRepository)
        {
            _config = config;
            _logger = logger;
            _employeeLoginRepository = employeeLoginRepository;
        }

        /*public async Task<string> Validate(string emailId, string password)
        {
            string token = string.Empty;
            bool result = await _employeeLoginRepository.Validate(emailId, password);
            if (result)
            {
                token = GenerateToken(emailId);
            }
            return token;
        }

        private string GenerateToken(string emailId)
        {
            try
            {
                _logger.LogInformation($"Begin GenerateToken method for user: {emailId}");

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>()
                {
                new Claim(ClaimTypes.Name, emailId)
                };

                foreach (var role in SourceRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var Sectoken = new JwtSecurityToken(_config["Jwt:Issuer"],
                  _config["Jwt:Issuer"],
                  claims,
                  expires: DateTime.Now.AddMinutes(120),
                  signingCredentials: credentials);

                string token = new JwtSecurityTokenHandler().WriteToken(Sectoken);
                _logger.LogInformation($"End  GenerateToken method.");
                return token;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }*/

        public async Task<string> Validate(string emailId, string password)
        {
            string token = string.Empty;
            bool isValidUser = await _employeeLoginRepository.Validate(emailId, password);

            if (isValidUser)
            {
                // Fetch the role for the authenticated user from the TblRole table
                string role = await _employeeLoginRepository.GetUserRole(emailId);
                token = GenerateToken(emailId, role); // Pass the role
            }
            return token;
        }

        private string GenerateToken(string emailId, string role)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, emailId),
            new Claim(ClaimTypes.Role, role) // Add the user's role to the claims
        };

                var token = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Issuer"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

    }
}




