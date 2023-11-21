using System;
using System.Threading.Tasks;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MvcMovie.Models;

public interface ISendMailService
{
    Task SendMail(MailContent mailContent);

    Task SendEmailAsync(string email, string subject, string htmlMessage);
}




public class SendMailService : ISendMailService
{
    private MailSettings _mailSettings;

    private readonly ILogger<SendMailService> _logger;
    private readonly IConfiguration _configuration;


    // mailSetting được Inject qua dịch vụ hệ thống
    // Có inject Logger để xuất log
    public SendMailService(IConfiguration configuration, ILogger<SendMailService> logger)
    {
        _configuration = configuration;
        _logger = logger;
        _logger.LogInformation("[LOG] Create SendMailService");
        _mailSettings = _configuration.GetSection("MailSettings").Get<MailSettings>();
    }

    // Gửi email, theo nội dung trong mailContent
    public async Task SendMail(MailContent mailContent)
    {
        _logger.LogInformation("[LOG] MailSettings: " + _mailSettings.DisplayName);
        var email = new MimeMessage()
        {
            Sender = new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail),
            Subject = mailContent.Subject,
            Body = new BodyBuilder { HtmlBody = mailContent.Body }.ToMessageBody()
        };
        email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail));
        email.To.Add(MailboxAddress.Parse(mailContent.To));

        // dùng SmtpClient của MailKit
        using var smtp = new MailKit.Net.Smtp.SmtpClient();

        try
        {
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
        }
        catch (Exception ex)
        {
            // Gửi mail thất bại, nội dung email sẽ lưu vào thư mục MailErrors
            Directory.CreateDirectory("MailErrors");
            var emailsavefile = string.Format(@"MailErrors/{0}.eml", Guid.NewGuid());
            await email.WriteToAsync(emailsavefile);

            _logger.LogInformation("[LOG] Lỗi gửi mail, lưu tại - " + emailsavefile);
            _logger.LogError(ex.Message);
        }

        smtp.Disconnect(true);

        _logger.LogInformation("[LOG] send mail to " + mailContent.To);

    }
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        await SendMail(new MailContent()
        {
            To = email,
            Subject = subject,
            Body = htmlMessage
        });
    }
}