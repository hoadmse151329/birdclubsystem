using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string _from, string _to, string _subject, string _body, SmtpClient client);
        Task<bool> SendEmailLocalSmtp(string _from, string _to, string _subject, string _body);
        Task<bool> SendEmailGoogleSmtpAsync(string _from, string _to, string _subject, string _body, string _gmailsend, string? _gmailpassword);
    }
}
