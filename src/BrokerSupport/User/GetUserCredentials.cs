using BrokerSupport.User.Interfaces;
using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.Models.Broker.Requests.User;
using LT.DigitalOffice.Models.Broker.Responses.User;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrokerSupport.User
{
  public class GetUserCredentials<T> : IGetUserCredentials<T> where T : class
  {
    private readonly ILogger<GetUserCredentials<T>> _logger;
    private readonly IRequestClient<IGetUserCredentialsRequest> _requestClient;

    public GetUserCredentials(
      ILogger<GetUserCredentials<T>> logger,
      IRequestClient<IGetUserCredentialsRequest> requestClient)
    {
      _logger = logger;
      _requestClient = requestClient;
    }

    public async Task<IGetUserCredentialsResponse> ExecuteAsync(
      List<string> errors,
      string loginData)
    {
      try
      {
        var response = await _requestClient.GetResponse<IOperationResult<IGetUserCredentialsResponse>>(
          IGetUserCredentialsRequest.CreateObj(loginData));

        if (response.Message.IsSuccess && response.Message.Body != null)
        {
          return response.Message.Body;
        }

        _logger.LogWarning(
          "Errors while getting user credentials with login data '{LoginData}'.\n Reason: {Errors}",
          loginData,
          string.Join('\n', response.Message.Errors));
      }
      catch (Exception exc)
      {

        _logger.LogError(
          exc,
          "Сan't get user credentials with login data '{LoginData}'",
          loginData);
      }

      errors.Add("Сan't get user credentials. Please try again later.");

      return null;
    }
  }
}
