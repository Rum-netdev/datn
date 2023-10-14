using EcommercialWebApp.Core.Helpers.Email;
using EcommercialWebApp.Data;
using EcommercialWebApp.Handler.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommercialWebApp.Handler.Authentication.Commands
{
    public class ForgotPasswordCommand : ICommand<ForgotPasswordCommandResult>
    {
        public string UserName { get; set; }
    }

    public class ForgotPasswordCommandHandler : ICommandHandler<ForgotPasswordCommand, ForgotPasswordCommandResult>
    {
        private readonly ApplicationDbContext _db;
        private readonly IEmailSender _emailSender;

        public ForgotPasswordCommandHandler(ApplicationDbContext db,
            IEmailSender emailSender)
        {
            _db = db;
            _emailSender = emailSender;
        }

        public async Task<ForgotPasswordCommandResult> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            ForgotPasswordCommandResult result = new ForgotPasswordCommandResult();
            if (string.IsNullOrEmpty(request.UserName))
            {
                result.IsSuccess = false;
                result.Message = "The username must not be empty";

                return result;
            }

            var user = _db.Users.Where(t => t.UserName.Equals(request.UserName) || t.Email.Equals(request.UserName))
                .FirstOrDefault();
            
            if (user == null)
            {
                result.IsSuccess = false;
                result.Message = $"UserName {request.UserName} does not existed";

                return result;
            }

            // Handle send change password url to email

            return result;
        }
    }

    public class ForgotPasswordCommandResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
