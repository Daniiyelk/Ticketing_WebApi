using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace Ticketing.Core.Services.Interfaces;

public class MailServices : IMailServices
{
    private readonly string _mailFrom = string.Empty;

    public MailServices(IConfiguration configuration)
    {
        _mailFrom = configuration["MailSetting:MailFrom"];
    }
    
    public void SendMail(string subject, string body,string mailto)
    {
        // in the beginning of the file 
        
        MailAddress to = new MailAddress(mailto);
        MailAddress from = new MailAddress(_mailFrom);
        MailMessage message = new MailMessage(from, to);
        message.Subject = subject;
        message.Body = body;
        SmtpClient client = new SmtpClient("smtp.server.address", 2525)
        {
            Credentials = new NetworkCredential("smtp_username", "smtp_password"),
            EnableSsl = true
        // specify whether your host accepts SSL connections
        };
        // code in brackets above needed if authentication required
        try
        {
            client.Send(message);
        }
        catch (SmtpException ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }
}