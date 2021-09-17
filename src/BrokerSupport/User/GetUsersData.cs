using BrokerSupport.User.Interfaces;
using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.Models.Broker.Models;
using LT.DigitalOffice.Models.Broker.Requests.User;
using LT.DigitalOffice.Models.Broker.Responses.User;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrokerSupport.User
{
  public class GetUsersData<T> : IGetUsersData<T> where T : class
  {
    private readonly ILogger<GetUsersData<T>> _logger;
    private readonly IRequestClient<IGetUsersDataRequest> _requestClient;

    public GetUsersData(
      ILogger<GetUsersData<T>> logger,
      IRequestClient<IGetUsersDataRequest> requestClient)
    {
      _logger = logger;
      _requestClient = requestClient;
    }

    public async Task<List<UserData>> ExecuteAsync(
      List<string> errors,
      List<Guid> usersIds)
    {
      if (usersIds == null || !usersIds.Any())
      {
        return null;
      }

      try
      {
        var response = await _requestClient.GetResponse<IOperationResult<IGetUsersDataResponse>>(
          IGetUsersDataRequest.CreateObj(usersIds));

        if (response.Message.IsSuccess && response.Message.Body != null)
        {
          return response.Message.Body.UsersData;
        }

        _logger.LogWarning(
          "Errors while getting users data by users ids: {UsersIds}.\n Reason: {Errors}",
          string.Join(", ", usersIds),
          string.Join('\n', response.Message.Errors));
      }
      catch (Exception exc)
      {
        _logger.LogError(
          exc,
          "Сan't get users data by users ids: {UsersIds}.",
          string.Join(", ", usersIds));
      }

      errors.Add("Сan't get users data. Please try again later.");

      return null;
    }
  }
}
