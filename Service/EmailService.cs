using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MimeKit;
using Org.BouncyCastle.Asn1.Ocsp;
using Service.Abstractions;
using Service.Requests;
using Service.Settings;
using System.Net;

namespace Service
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> options)
        {
            _settings = options.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task SendEmailAsync(EmailRequest request)
        {
            MimeMessage message = new();

            // setup sender & receivers 
            message.From.Add(MailboxAddress.Parse(_settings.Email));
            AddMainReceivers(message, request.Receivers.To);
            TryAddCCs(message, request.Receivers.Cc);
            TryAddBccs(message, request.Receivers.Bcc);

            // setup content
            message.Subject = request.Subject;
            BodyBuilder builder = new()
            {
                HtmlBody = request.Body.Message
            };
            TryAddAttachements(builder, request.Body.Attachements);
            message.Body = builder.ToMessageBody();

            SmtpClient client = new();
            try
            {
                await client.ConnectAsync(_settings.Server, _settings.Port, true);
                await client.AuthenticateAsync(new NetworkCredential
                {
                    UserName = _settings.Email,
                    Password = _settings.Password
                });
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                client.Dispose();
            }
        }

        private static void AddMainReceivers(MimeMessage message, IEnumerable<string> to)
        {
            foreach (string email in to)
            {
                message.To.Add(MailboxAddress.Parse(email));
            }
        }

        private static void TryAddCCs(MimeMessage message, IEnumerable<string>? ccs)
        {
            if (ccs is null) return;
            foreach (string email in ccs)
            {
                message.Cc.Add(MailboxAddress.Parse(email));
            }
        }

        private static void TryAddBccs(MimeMessage message, IEnumerable<string>? bccs)
        {
            if (bccs is null) return;
            foreach (string email in bccs)
            {
                message.Cc.Add(MailboxAddress.Parse(email));
            }
        }

        private static async void TryAddAttachements(BodyBuilder builder, IEnumerable<IFormFile>? attachements)
        {
            if(attachements is null) return;
            foreach(IFormFile attachement in attachements)
            {
                using MemoryStream stream = new();
                await attachement.CopyToAsync(stream);
                builder.Attachments.Add(attachement.FileName, stream.ToArray());
            }
        }

    }
}
