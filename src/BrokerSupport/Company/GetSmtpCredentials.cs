using BrokerSupport.Company.Interfaces;
using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.Models.Broker.Requests.Company;
using LT.DigitalOffice.Models.Broker.Responses.Company;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Company
{
  public class GetSmtpCredentials<T> : IGetSmtpCredentials<T> where T : class
  {
    private readonly ILogger<GetSmtpCredentials<T>> _logger;
    private readonly IRequestClient<IGetSmtpCredentialsRequest> _requestClient;

    public GetSmtpCredentials(
      ILogger<GetSmtpCredentials<T>> logger,
      IRequestClient<IGetSmtpCredentialsRequest> requestClient)
    {
      _logger = logger;
      _requestClient = requestClient;
    }

    public async Task<IGetSmtpCredentialsResponse> ExecuteAsync(
      List<string> errors)
    {
      try
      {
        var response = await _requestClient.GetResponse<IOperationResult<IGetSmtpCredentialsResponse>>(
          IGetSmtpCredentialsRequest.CreateObj());

        if (response.Message.IsSuccess && response.Message.Body != null)
        {
          return response.Message.Body;
        }

        _logger.LogWarning(
          "Errors while getting smtp credentials.\n Reason: {Errors}",
          string.Join('\n', response.Message.Errors));
      }
      catch (Exception exc)
      {
        _logger.LogError(exc, "Сan't get smtp credentials.");

      }

      errors.Add("Сan't get smtp credentials. Please try again later.");

      return null;
    }
  }
}
