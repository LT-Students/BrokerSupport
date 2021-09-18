using BrokerSupport.Extension;
using BrokerSupport.Message.Interfaces;
using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.Models.Broker.Enums;
using LT.DigitalOffice.Models.Broker.Requests.Message;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Message
{
  public class EmailSender : IEmailSender
  {
    private readonly ILogger<EmailSender> _logger;
    private readonly HttpContext _httpContext;
    private readonly IRequestClient<ISendEmailRequest> _requestClient;

    public EmailSender(
      ILogger<EmailSender> logger,
      IHttpContextAccessor httpContextAccessor,
      IRequestClient<ISendEmailRequest> requestClient)
    {
      _logger = logger;
      _httpContext = httpContextAccessor?.HttpContext;
      _requestClient = requestClient;
    }

    public async Task<bool> ExecuteAsync(
      List<string> errors,
      string email,
      Dictionary<string, string> templateValues,
      EmailTemplateType templateType = EmailTemplateType.Notification,
      string templateLanguage = "en",
      Guid? templateId = null)
    {
      if (string.IsNullOrWhiteSpace(email))
      {
        errors.Add("User does not have any linked email.");

        return false;
      }

      return await _requestClient.ProcessRequest<ISendEmailRequest, bool>(
        ISendEmailRequest.CreateObj(
          templateId,
          _httpContext?.GetUserId() ?? Guid.Empty,
          email,
          templateLanguage,
          templateType,
          templateValues),
        errors,
        _logger);
    }
  }
}
