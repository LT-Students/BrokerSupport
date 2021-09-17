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
using System.Linq;
using System.Threading.Tasks;

namespace BrokerSupport.Message
{
  // Надо утащить в другое место
  public static class Extensions
  {
    public static bool IsSuccess<T>(this Response<IOperationResult<T>> response)
    {
      return response != null && response.Message.IsSuccess;
    }

    public static async Task<T> ProcessRequest<U, T>(
      this IRequestClient<U> requestClient,
      object request,
      List<string> errors = null,
      ILogger logger = null) where U : class
    {
      IOperationResult<T> result = default;

      try
      {
        Response<IOperationResult<T>> response = await requestClient.GetResponse<IOperationResult<T>>(request);

        if (!response.IsSuccess())
        {
          errors?.Add("Request was not success.");
        }

        if (response.Message.Errors.Any())
        {
          logger?.LogWarning(
            "Errors while processing request:\n {Errors}",
            string.Join('\n', response.Message.Errors));
        }

        result = response.Message;
      }
      catch (Exception exc)
      {
        logger?.LogError(exc, "Can not process request.");
      }

      return result != null ? result.Body : default;
    }
  }

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
