using EcommercialWebApp.Core.Helpers.Email.Models;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommercialWebApp.Core.Helpers.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public SendMailResponse SendMail(SendMailRequest request)
        {
            var emailMessage = CreateEmailMessage(request.Message);
            return Send(emailMessage);
        }

        public async Task<SendMailResponse> SendMailAsync(SendMailRequest request)
        {
            var emailMessage = CreateEmailMessage(request.Message);
            return await SendAsync(emailMessage);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

            return emailMessage;
        }

        private SendMailResponse Send(MimeMessage emailMessage)
        {
            SendMailResponse response = new SendMailResponse();
            using (var smtpClient = new SmtpClient())
            {
                try
                {
                    smtpClient.Connect(host: _emailConfig.SmtpServer, port: _emailConfig.Port, useSsl: true);
                    smtpClient.AuthenticationMechanisms.Remove("XOAUTH2");
                    smtpClient.Authenticate(_emailConfig.UserName, _emailConfig.Password);

                    smtpClient.Send(emailMessage);

                    response.IsSuccess = true;
                    response.ExecutedTime = DateTime.Now;
                    response.From = emailMessage.From.Select(t => t.Name).FirstOrDefault();
                    response.To = emailMessage.To.Select(t => t.Name).FirstOrDefault();
                    response.Message = "Sending email successfully";
                }
                catch (Exception ex)
                {
                    response.IsSuccess = false;
                    response.ExecutedTime = DateTime.Now;
                    response.Message = ex.Message;
                }
                finally
                {
                    smtpClient.Disconnect(true);
                    smtpClient.Dispose();
                }

                return response;
            }
        }

        private async Task<SendMailResponse> SendAsync(MimeMessage emailMessage)
        {
            SendMailResponse response = new SendMailResponse();
            using(var smtpClient = new SmtpClient())
            {
                try
                {
                    await smtpClient.ConnectAsync(_emailConfig.SmtpServer);
                    smtpClient.AuthenticationMechanisms.Remove("XOAUTH2");
                    await smtpClient.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);

                    await smtpClient.SendAsync(emailMessage);

                    response.IsSuccess = true;
                    response.ExecutedTime = DateTime.Now;
                    response.From = emailMessage.From.Select(t => t.Name).FirstOrDefault();
                    response.To = emailMessage.To.Select(t => t.Name).FirstOrDefault();
                    response.Message = "Sending email successfully";
                }
                catch (Exception ex)
                {
                    response.IsSuccess = false;
                    response.ExecutedTime = DateTime.Now;
                    response.Message = ex.Message;
                }
                finally
                {
                    await smtpClient.DisconnectAsync(true);
                    smtpClient.Dispose();
                }
            }

            return response;
        }
    }
}
