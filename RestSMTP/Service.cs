using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace RestSMTP
{
    // TODO: Queue for emails being currently sent (return active task)
    // TODO: QUeue for sent emails recently to avoid duplicated e-mails (clear after a few seconds)
    public class Service
    {
        private readonly SmtpClient _client;
        private readonly MailAddress _sender;
        private readonly Settings _settings;

        public Service(IOptions<Settings> settings)
        {
            _settings = settings.Value;

            _client = new SmtpClient
            {
                Host = _settings.Host,
                Port = 587,
                EnableSsl = _settings.SSL,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_sender.Address, settings.Value.Password)
            };
        }
    }
}
