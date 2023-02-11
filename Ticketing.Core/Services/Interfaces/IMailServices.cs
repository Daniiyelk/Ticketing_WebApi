namespace Ticketing.Core.Services.Interfaces;

public interface IMailServices
{
    public void SendMail(string subject, string body,string mailto);
}