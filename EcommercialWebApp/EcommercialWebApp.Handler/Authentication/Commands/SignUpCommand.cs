using EcommercialWebApp.Core.Enums;
using EcommercialWebApp.Data;
using EcommercialWebApp.Data.Models.Commons;
using EcommercialWebApp.Handler.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;

namespace EcommercialWebApp.Handler.Authentication.Commands
{
    public class SignUpCommand : ICommand<SignUpCommandResult>
    {
        public string UserName { get; set; }
        public string? UserAvatar { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class SignUpCommandHandler : ICommandHandler<SignUpCommand, SignUpCommandResult>
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public SignUpCommandHandler(ApplicationDbContext db,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<SignUpCommandResult> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            SignUpCommandResult result = new SignUpCommandResult();

            // Do validating here (normal way or use FluentValidation)
            if (string.IsNullOrEmpty(request.UserName))
            {
                result.Message = "The username must not be empty";
                result.IsSuccess = false;

                return result;
            }
            if (string.IsNullOrEmpty(request.Password))
            {
                result.Message = "The password must not be empty";
                result.IsSuccess = false;

                return result;
            }
            if (string.IsNullOrEmpty(request.PasswordConfirmation))
            {
                result.Message = "The password confirmation must not be empty";
                result.IsSuccess = false;

                return result;
            }
            if (request.Password.Length < 8)
            {
                result.Message = "Your password is too weak, please choose another one";
                result.IsSuccess = false;

                return result;
            }
            if (!request.Password.Equals(request.PasswordConfirmation))
            {
                result.Message = "The password and password confirmation are not equals, please try again";
                result.IsSuccess = false;

                return result;
            }
            if (string.IsNullOrEmpty(request.FirstName))
            {
                result.Message = "Your first name must not be empty";
                result.IsSuccess = false;

                return result;
            }
            if (string.IsNullOrEmpty(request.LastName))
            {
                result.Message = "Your last name must not be empty";
                result.IsSuccess = false;

                return result;
            }
            if (string.IsNullOrEmpty(request.Email))
            {
                result.Message = "The email must not be empty";
                result.IsSuccess = false;

                return result;
            }
            if (!Regex.IsMatch(request.Email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
            {
                result.Message = "The email format is invalid, please try other";
                result.IsSuccess = false;

                return result;
            }

            // Check the username is existed
            if (_db.Users.Any(x => x.UserName.Equals(request.UserName)))
            {
                result.Message = "Username is used, please choose other username";
                result.IsSuccess = false;

                return result;
            }


            // Creating
            ApplicationUser user = new ApplicationUser();
            user.UserName = request.UserName;
            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;

            var createResult = await _userManager.CreateAsync(user, request.Password);

            result.IsSuccess = createResult.Succeeded;
            if (createResult.Errors != null && createResult.Errors.Count() > 0)
            {
                result.Errors = createResult.Errors
                    .Select(t => t.Description)
                    .ToList();
            }
            result.Message = createResult.Succeeded ? $"Sign up account {request.UserName} successfully" : $"Sign up account {request.UserName} failed";

            // Begin set role to user
            await SeedDefaultRoleToUserAsync(user);

            return result;
        }
        private async Task SeedDefaultRoleToUserAsync(ApplicationUser user)
        {
            CheckRoleExisted:
            // get default role
            var role = _db.Roles.Where(t => t.Name.Equals(RoleEnums.BasicUser.ToString())).FirstOrDefault();
            
            // Resolving if role is null
            if (role == null)
            {
                //_db.Roles.Add(new ApplicationRole()
                //{
                //    Name = RoleEnums.BasicUser.ToString()
                //});
                //await _db.SaveChangesAsync();

                await _roleManager.CreateAsync(new ApplicationRole()
                {
                    Name = RoleEnums.BasicUser.ToString()
                });

                goto CheckRoleExisted;
            }

            await _userManager.AddToRoleAsync(user, RoleEnums.BasicUser.ToString());
        }
    }

    public class SignUpCommandResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public ICollection<string> Errors { get; set; }
    }
}
