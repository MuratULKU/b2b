using Microsoft.Extensions.Hosting;
using System.Net.Mail;
using System.Net;
using Business.Concrete;
using Business.Abstract;
using Entity;

namespace SendEMail
{
    public class EMailSender
    {
        private readonly string _host;
        private readonly int _port;
        private readonly bool _enableSSL;
        private readonly string _userName;
        private readonly string _password;
        
        public  EMailSender(string host, int port, bool enableSSl, string userNmae, string password)
        {
            _host = host;
            _port = port;
            _enableSSL = enableSSl;
            _userName = userNmae;
            _password = password;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var mesaj = new MailMessage(_userName, email, subject, htmlMessage);
            mesaj.IsBodyHtml = true;
            mesaj.Body = htmlMessage;
            var smtpClient = new SmtpClient(_host, _port);
            smtpClient.Credentials = new NetworkCredential(_userName, _password);
            smtpClient.EnableSsl = _enableSSL;
            await smtpClient.SendMailAsync(mesaj);
        }
    }
}
