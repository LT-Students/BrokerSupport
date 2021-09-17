using BrokerSupport.Message.Interfaces;
using LT.DigitalOffice.Kernel.Broker;
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
  public class SendEmail<T> : ISendEmail<T> where T : class
  {
    private readonly ILogger<SendEmail<T>> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRequestClient<ISendEmailRequest> _requestClient;

    public SendEmail(
      ILogger<SendEmail<T>> logger,
      IHttpContextAccessor httpContextAccessor,
      IRequestClient<ISendEmailRequest> requestClient)
    {
      _logger = logger;
      _httpContextAccessor = httpContextAccessor;
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

      if (string.IsNullOrEmpty(email))
      {
        errors.Add("User does not have any linked email.");

        return false;
      }

      try
      {
        var response = await _requestClient.GetResponse<IOperationResult<bool>>(
          ISendEmailRequest.CreateObj(
            templateId,
            _httpContextAccessor.HttpContext.GetUserId(),
            email,
            templateLanguage,
            templateType,
            templateValues));

        if (response.Message.IsSuccess && response.Message.Body)
        {
          return true;
        }

        _logger.LogWarning(
          "Errors while sending email to '{Email}':\n {Errors}",
          email,
          string.Join('\n', response.Message.Errors));
      }
      catch (Exception exc)
      {
        _logger.LogError(exc, "Can not send email to '{Email}'", email);
      }

      errors.Add("Can not send email to '{email}'.Email placed in resend queue and will be resent in 1 hour.");

      return false;
    }
  }
}
