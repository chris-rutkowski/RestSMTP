using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using RestSMTP.Dtos;
using RestSMTP.Measurement;

namespace RestSMTP
{
    // TODO: Queue for emails being currently sent (return active task)
    // TODO: QUeue for sent emails recently to avoid duplicated e-mails (clear after a few seconds)
    public class Service
    {
        private readonly SmtpClient _client;
        private readonly ILogger<Service> _logger;
        private readonly IMeasurementService _measurementService;
        private readonly SMTPSettings _smtpSettings;

        public Service(
            ILogger<Service> logger,
            IMeasurementService measurementService,
            IOptions<SMTPSettings> smtpSettings)
        {
            _logger = logger;
            _measurementService = measurementService;
            _smtpSettings = smtpSettings.Value;

            _client = new SmtpClient
            {
                Host = _smtpSettings.Host,
                Port = 587,
                EnableSsl = _smtpSettings.SSL,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(smtpSettings.Value.Username, smtpSettings.Value.Password)
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
            {
                _ = _measurementService.CountEmailResult(EmailResultType.Invalid);
                throw new ValidationException();
            }

            var message = new MailMessage();
            message.From = new MailAddress(dto.FromEmail!, dto.FromName);
            message.To.Add(new MailAddress(dto.To!));
            message.Subject = dto.Subject;
            message.Body = dto.Content ?? "";
            message.IsBodyHtml = dto.IsContentHTML;

            if (dto.ReplyTo != null)
                message.ReplyToList.Add(new MailAddress(dto.ReplyTo));

            try
            {
                await _client.SendMailAsync(message);
                _ = _measurementService.CountEmailResult(EmailResultType.Success);
                _logger.LogInformation($"Sent `{message.Subject}`.");
            }
            catch (Exception e)
            {
                _ = _measurementService.CountEmailResult(EmailResultType.Failure);
                _logger.LogError(e, $"Error sending `{message.Subject}`.");
                throw;
            }
        }
    }
}
