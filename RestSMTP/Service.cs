using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace RestSMTP
{
    public class Service
    {
        private readonly SmtpClient _client;
        private readonly MailAddress _sender;
        private readonly Settings _settings;

        public Service(IOptions<Settings> settings)
        {
            _client = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_sender.Address, settings.Value.Password)
            };

            _settings = settings.Value;
        }
    }
}
