using BrokerSupport.User.Interfaces;
using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.Models.Broker.Common;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrokerSupport.User
{
  public class CheckUsers<T> : ICheckUses<T> where T : class
  {
    private readonly ILogger<CheckUsers<T>> _logger;
    private readonly IRequestClient<ICheckUsersExistence> _requestClient;

    public CheckUsers(
      ILogger<CheckUsers<T>> logger,
      IRequestClient<ICheckUsersExistence> requestClient)
    {
      _logger = logger;
      _requestClient = requestClient;
    }

    public async Task<List<Guid>> ExecuteAsync(
      List<string> errors,
      List<Guid> usersIds)
    {
      if (usersIds == null || !usersIds.Any())
      {
        return null;
      }

      try
      {
        var response = await _requestClient.GetResponse<IOperationResult<ICheckUsersExistence>>(
          ICheckUsersExistence.CreateObj(usersIds));

        if (response.Message.IsSuccess && response.Message.Body != null)
        {
          return response.Message.Body.UserIds;
        }

        _logger.LogWarning(
          "Errors while checking users existence with users ids: {UsersIds}.\n Reason: {Errors}",
          string.Join(", ", usersIds),
          string.Join('\n', response.Message.Errors));
      }
      catch (Exception exc)
      {

        _logger.LogError(
          exc,
          "Сan't check users existence with users ids: {USersIds}.",
          usersIds);
      }

      errors.Add("Сan't check users existence. Please try again later.");

      return null;
    }
  }
}
