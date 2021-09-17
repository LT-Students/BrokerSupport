using BrokerSupport.Message.Interfaces;
using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.Models.Broker.Requests.Message;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Message
{
  public class UpdateSmtpCredentials<T> : IUpdateSmtpCredentials<T> where T : class
  {
    private readonly ILogger<UpdateSmtpCredentials<T>> _logger;
    private readonly IRequestClient<IUpdateSmtpCredentialsRequest> _requestClient;

    public UpdateSmtpCredentials(
      ILogger<UpdateSmtpCredentials<T>> logger,
      IRequestClient<IUpdateSmtpCredentialsRequest> requestClient)
    {
      _logger = logger;
      _requestClient = requestClient;
    }

    public async Task<bool> ExecuteAsync(
      List<string> errors,
      string host,
      int port,
      bool enableSsl,
      string email,
      string password)
    {
      try
      {
        var response = await _requestClient.GetResponse<IOperationResult<bool>>(
          IUpdateSmtpCredentialsRequest.CreateObj(
            host,
            port,
            enableSsl,
            email,
            password));

        if (response.Message.IsSuccess && response.Message.Body)
        {
          return true;
        }

        _logger.LogWarning(
          "Errors while updating smtp credentials.\n Reason: {Errors}",
          string.Join('\n', response.Message.Errors));
      }
      catch (Exception exc)
      {
        _logger.LogError(exc, "Сan't update smtp credentials.");
      }

      errors.Add("Сan't update smtp credentials. Please try again later.");

      return false;
    }
  }
}
