using BrokerSupport.Extension;
using BrokerSupport.Message.Interfaces;
using LT.DigitalOffice.Models.Broker.Requests.Message;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Message
{
  public class SmtpCredentialsUpdater : ISmtpCredentialsUpdater
  {
    private readonly ILogger<SmtpCredentialsUpdater> _logger;
    private readonly IRequestClient<IUpdateSmtpCredentialsRequest> _requestClient;

    public SmtpCredentialsUpdater(
      ILogger<SmtpCredentialsUpdater> logger,
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
      if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
      {
        return false;
      }

      return await _requestClient.ProcessRequest<IUpdateSmtpCredentialsRequest, bool>(
        IUpdateSmtpCredentialsRequest.CreateObj(
          host,
          port,
          enableSsl,
          email,
          password),
        errors,
        _logger);
    }
  }
}
