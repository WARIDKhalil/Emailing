using Service.Requests;

namespace Service.Abstractions
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailRequest request);
    }
}
