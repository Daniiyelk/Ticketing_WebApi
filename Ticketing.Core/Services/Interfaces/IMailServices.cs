namespace Ticketing.Core.Services.Interfaces;

public interface IMailServices
{
    public string SendMail(string subject, string body,string mailto);
}