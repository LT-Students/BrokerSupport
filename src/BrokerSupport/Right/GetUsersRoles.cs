using BrokerSupport.Right.Interfaces;
using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.Models.Broker.Models;
using LT.DigitalOffice.Models.Broker.Requests.Rights;
using LT.DigitalOffice.Models.Broker.Responses.Rights;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrokerSupport.Right
{
  public class GetUsersRoles<T> : IGetUsersRoles<T> where T : class
  {
    private readonly ILogger<GetUsersRoles<T>> _logger;
    private readonly IRequestClient<IGetUserRolesRequest> _requestClient;

    public GetUsersRoles(
      ILogger<GetUsersRoles<T>> logger,
      IRequestClient<IGetUserRolesRequest> requestClient)
    {
      _logger = logger;
      _requestClient = requestClient;
    }

    public async Task<List<RoleData>> ExecuteAsync(
      List<string> errors,
      List<Guid> usersIds)
    {
      if (usersIds == null || !usersIds.Any() || usersIds.Contains(Guid.Empty))
      {
        return null;
      }

      try
      {
        var response = await _requestClient.GetResponse<IOperationResult<IGetUserRolesResponse>>(
          IGetUserRolesRequest.CreateObj(usersIds));

        if (response.Message.IsSuccess && response.Message.Body != null)
        {
          return response.Message.Body.Roles;
        }

        _logger.LogWarning("Errors while geting roles for users with ids: {Ids}.\n Reason: {Errors}",
          string.Join(", ", usersIds),
          string.Join('\n', response.Message.Errors));
      }
      catch (Exception exc)
      {
        _logger.LogError(exc, "Сan't get roles for users with ids: {Ids}", string.Join(", ", usersIds));
      }

      errors.Add("Сan't get roles.Please try again later.");

      return null;
    }
  }
}
