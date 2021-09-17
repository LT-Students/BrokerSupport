using BrokerSupport.Company.Interfaces;
using BrokerSupport.Extension;
using LT.DigitalOffice.Models.Broker.Requests.Company;
using LT.DigitalOffice.Models.Broker.Responses.Company;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.Company
{
  public class SmtpCredentialsGetter : ISmtpCredentialsGetter
  {
    private readonly ILogger<SmtpCredentialsGetter> _logger;
    private readonly IRequestClient<IGetSmtpCredentialsRequest> _requestClient;

    public SmtpCredentialsGetter(
      ILogger<SmtpCredentialsGetter> logger,
      IRequestClient<IGetSmtpCredentialsRequest> requestClient)
    {
      _logger = logger;
      _requestClient = requestClient;
    }

    public async Task<IGetSmtpCredentialsResponse> ExecuteAsync(
      List<string> errors)
    {
      return await _requestClient.ProcessRequest<IGetSmtpCredentialsRequest, IGetSmtpCredentialsResponse>(
        IGetSmtpCredentialsRequest.CreateObj(), errors, _logger);
    }
  }
}
