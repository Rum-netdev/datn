using EcommercialWebApp.Core.Helpers.Email.Models;
namespace EcommercialWebApp.Core.Helpers.Email
{
    public interface IEmailSender
    {
        SendMailResponse SendMail(SendMailRequest request);
        Task<SendMailResponse> SendMailAsync(SendMailRequest request);
    }
}
