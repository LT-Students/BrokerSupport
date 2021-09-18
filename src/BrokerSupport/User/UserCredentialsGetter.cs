using BrokerSupport.Extension;
using BrokerSupport.User.Interfaces;
using LT.DigitalOffice.Models.Broker.Requests.User;
using LT.DigitalOffice.Models.Broker.Responses.User;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerSupport.User
{
  public class UserCredentialsGetter : IUserCredentialsGetter
  {
    private readonly ILogger<UserCredentialsGetter> _logger;
    private readonly IRequestClient<IGetUserCredentialsRequest> _requestClient;

    public UserCredentialsGetter(
      ILogger<UserCredentialsGetter> logger,
      IRequestClient<IGetUserCredentialsRequest> requestClient)
    {
      _logger = logger;
      _requestClient = requestClient;
    }

    public async Task<IGetUserCredentialsResponse> ExecuteAsync(
      List<string> errors,
      string loginData)
    {
      if (string.IsNullOrEmpty(loginData))
      {
        return null;
      }

      return await _requestClient.ProcessRequest<IGetUserCredentialsRequest, IGetUserCredentialsResponse>(
        IGetUserCredentialsRequest.CreateObj(
          loginData),
        errors,
        _logger);
    }
  }
}
