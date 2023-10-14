using EcommercialWebApp.Core.Claims;
using EcommercialWebApp.Data.Models.Commons;
using EcommercialWebApp.Handler.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace EcommercialWebApp.Handler.Authentication.Commands
{
    public class LoginCommand : ICommand<LoginCommandResult>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; } = false;
    }

    public class LoginCommandHandler : ICommandHandler<LoginCommand, LoginCommandResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public LoginCommandHandler(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<LoginCommandResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            LoginCommandResult result = new LoginCommandResult();
            if (string.IsNullOrEmpty(request.UserName))
            {
                result.IsLoggedIn = false;
                result.Message = "Please input your email or username for signing in";

                return result;
            }

            if (string.IsNullOrEmpty(request.Password))
            {
                result.IsLoggedIn = false;
                result.Message = "Your password must not be empty";

                return result;
            }

            // For validating username is a default username or an email
            Regex reg = new Regex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$");
            ApplicationUser user = null;
            if (reg.IsMatch(request.UserName))
            {
                user = await _userManager.FindByEmailAsync(request.UserName);
            }
            else
            {
                user = await _userManager.FindByNameAsync(request.UserName);
            }

            if (user == null)
            {
                result.IsLoggedIn = false;
                result.Message = "Username or email is not exist";

                return result;
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, false);

            if (signInResult.IsLockedOut)
            {
                result.Message = "This account is locked out";
                result.IsLoggedIn = false;
            }
            else if (signInResult.RequiresTwoFactor)
            {
                result.Message = "Account require two factors";
                result.IsLoggedIn = false;
            }
            else if(signInResult.IsNotAllowed)
            {
                result.Message = "Account is not allowed";
                result.IsLoggedIn = false;
            }
            else
            {
                result.TokenAuth = await SetSuccessLoginToken(user);
                result.Message = "Sign in successfully";
                result.UserId = user.Id;
                result.IsLoggedIn = true;

                return result;
            }

            // All the cases are skipped
            result.Message = "UserName or password are incorrect, please try again";
            result.IsLoggedIn = false;
            return result;
        }

        private async Task<string> SetSuccessLoginToken(ApplicationUser user)
        {
            // Get user roles
            var roles = string.Join(",", await _userManager.GetRolesAsync(user));

            // Generate security key
            string tokenKey = _configuration.GetSection("JwtConfiguration:SignInKey").Value;
            var securityKey = Encoding.UTF8.GetBytes(tokenKey);
            var symmetricSecurityKey = new SymmetricSecurityKey(securityKey);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = _configuration.GetSection("JwtConfiguration:ValidIssuer").Value,
                Audience = _configuration.GetSection("JwtConfiguration:ValidAudience").Value,
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(IdentityClaims.UserId, user.Id.ToString()),
                    new Claim(IdentityClaims.Roles, roles)
                }),
                Expires = DateTime.Now.AddMinutes(15),
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);

            return jwtToken;
        }
    }

    public class LoginCommandResult
    {
        public bool IsLoggedIn { get; set; }
        public int UserId { get; set; }
        public string TokenAuth { get; set; }
        public string Message { get; set; }
    }
}
