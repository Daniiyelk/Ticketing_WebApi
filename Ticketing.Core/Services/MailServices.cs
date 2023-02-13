using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Security;
using MimeKit.Text;
using MailKit.Net.Smtp;

namespace Ticketing.Core.Services.Interfaces;

public class MailServices : IMailServices
{
    private string? _mailFrom = String.Empty;
    private string? _mailFromPassword = String.Empty;
    private string? _mailHost = String.Empty;

    public MailServices(IConfiguration configuration)
    {
        _mailFrom = configuration["MailSetting:MailFrom"];
        _mailFromPassword = configuration["MailSetting:MailFromPassword"];
        _mailHost = configuration["MailSetting:MailHost"];
    }
    
    public string SendMail(string subject, string body,string mailto)
    {
        //create a new instance of MimeMessage and store data init
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_mailFrom));
        email.To.Add(MailboxAddress.Parse(mailto));

        //
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Html) { Text = body };
        
        //
        using var smtp = new SmtpClient();
        smtp.Connect(_mailHost, 587, SecureSocketOptions.StartTls);
        smtp.Authenticate(_mailFrom, _mailFromPassword);
        smtp.Send(email);
        smtp.Disconnect(true);

        return "Mail Sent!";
    }
}