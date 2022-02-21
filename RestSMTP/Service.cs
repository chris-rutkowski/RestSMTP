using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestSMTP.Dtos;

namespace RestSMTP
{
    // TODO: Queue for emails being currently sent (return active task)
    // TODO: QUeue for sent emails recently to avoid duplicated e-mails (clear after a few seconds)
    public class Service
    {
        private readonly SmtpClient _client;
        private readonly ILogger<Service> _logger;
        private readonly Settings _settings;

        public Service(
            ILogger<Service> logger,
            IOptions<Settings> settings)
        {
            _logger = logger;
            _settings = settings.Value;

            _client = new SmtpClient
            {
                Host = _settings.Host,
                Port = 587,
                EnableSsl = _settings.SSL,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(settings.Value.Username, settings.Value.Password)
            };
        }

        private bool ValidateDto(EmailDto dto)
        {
            if (!Validate.Email(dto.FromEmail))
                return false;

            if (!Validate.Email(dto.To))
                return false;

            if (dto.ReplyTo != null && !Validate.Email(dto.ReplyTo))
                return false;

            if (!Validate.Subject(dto.Subject))
                return false;

            return true;
        }

        public async Task Send(EmailDto dto)
        {
            if (!ValidateDto(dto))
                throw new ValidationException();

            var message = new MailMessage();
            message.From = new MailAddress(dto.FromEmail, dto.FromName);
            message.To.Add(new MailAddress(dto.To));
            message.Subject = dto.Subject;
            message.Body = dto.Content ?? "";
            message.IsBodyHtml = dto.IsContentHTML;

            if (dto.ReplyTo != null)
                message.ReplyToList.Add(new MailAddress(dto.ReplyTo));

            try
            {
                await _client.SendMailAsync(message);
                _logger.LogInformation($"Sent `{message.Subject}`.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error sending `{message.Subject}`.");
                throw;
            }
        }
    }
}
