
using System.Net.Mail;
using System.Net;

using Business.Abstract;


namespace SendEMail
{
    public class EMailSender
    {
        private readonly string host;
        private readonly int port;
        private readonly bool enableSSL;
        private readonly string userName;
        private readonly string password;
        
        public EMailSender(FirmParamService mailParamService)
        {
            this.host = mailParamService.ToString(11);
            this.port = Convert.ToInt32(mailParamService.ToString(12));
            this.enableSSL = mailParamService.ToBoolean(13);
            this.userName = mailParamService.ToString(14);
            this.password = mailParamService.ToString(15);

        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var mesaj = new MailMessage(userName, email, subject, htmlMessage);
            mesaj.IsBodyHtml = true;
            mesaj.Body = htmlMessage;
            var smtpClient = new SmtpClient(host, port);
            smtpClient.Credentials = new NetworkCredential(userName, password);
            smtpClient.EnableSsl = enableSSL;
            await smtpClient.SendMailAsync(mesaj);
        }
    }
}
